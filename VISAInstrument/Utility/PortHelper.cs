using Ivi.Visa;
using NationalInstruments.Visa;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace VISAInstrument.Utility
{
    internal class PortHelper
    {
        private static string ToStringFromPortType(PortType portType)
        {
            switch (portType)
            {
                case PortType.Usb: return "USB";
                case PortType.Gpib: return "GPIB";
                case PortType.Lan: return "TCPIP";
                case PortType.None:return "";
                case PortType.Rs232:
                default: return "ASRL";
            }
        }

        public static string[] FindAddresses(PortType portType)
        {
            IEnumerable<string> result = new List<string>();
            try
            {
                ResourceManager manager = new ResourceManager();
                result = manager.Find($"{ToStringFromPortType(portType)}?*INSTR");
            }catch(Exception ex)
            {
                if (!(ex is NativeVisaException))
                {
                    if (ex.InnerException != null) throw ex.InnerException;
                    throw;
                }
            }

            return result.ToArray().Where(n=>!n.Contains("//")).ToArray();
        }

        public static string[] FindRs232Type(string[] addresses)
        {
            List<string> list = new List<string>();
            foreach (var address in addresses)
            {
                try
                {
                    ResourceManager manager = new ResourceManager();
                    ParseResult result = manager.Parse(address);
                    list.Add(result.AliasIfExists);
                }
                catch
                {
                    list.Add(address);
                }
            }
            return list.ToArray();
        }

        public static bool OpenIpAddress(string address,out string fullAddress)
        {
            bool result = false;
            string addressTemp = $"TCPIP0::{address}::INSTR";
            fullAddress = addressTemp;
            try
            {
                using (new TcpipSession(addressTemp))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }

        public static string[] FindAddresses()
        {
           return FindAddresses(PortType.None);
        }
    }
}
