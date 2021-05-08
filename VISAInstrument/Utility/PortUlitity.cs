using System;
using System.Collections.Generic;
using System.Linq;
using Ivi.Visa;
using NationalInstruments.Visa;

namespace VISAInstrument.Port
{
    enum PortType
    {
        RS232, USB, GPIB, LAN,None
    }

    class PortUltility
    {
        private static string ToStringFromPortType(PortType portType)
        {
            switch (portType)
            {
                case PortType.USB: return "USB";
                case PortType.GPIB: return "GPIB";
                case PortType.LAN: return "TCPIP";
                case PortType.None:return "";
                case PortType.RS232:
                default: return "ASRL";
            }
        }

        public static string[] FindAddresses(PortType portType)
        {
            IEnumerable<string> result = new List<string>();
            try
            {
                result = GlobalResourceManager.Find($"{ToStringFromPortType(portType)}?*INSTR");
            }catch(Exception ex)
            {
                if (!(ex is NativeVisaException))
                {
                    if (ex.InnerException != null) throw ex.InnerException;
                    else throw ex;
                }
            }

            return result.ToArray().Where(n=>!n.Contains("//")).ToArray();
        }

        public static string[] FindRS232Type(string[] addresses)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < addresses.Length; i++)
            {
                try
                {
                    GlobalResourceManager.TryParse(addresses[i], out ParseResult result);
                    list.Add(result.AliasIfExists);
                }
                catch
                {
                    //list.Add("None");
                }
            }
            return list.ToArray();
        }

        public static bool OpenIPAddress(string address,out string fullAddress)
        {
            bool result = false;
            string addressTemp = $"TCPIP0::{address}::INSTR";
            fullAddress = addressTemp;
            try
            {
                using (TcpipSession session = new TcpipSession(addressTemp))
                {
                    result = true;
                }
            }
            catch{ }
            return result;
        }

        public static string[] FindAddresses()
        {
           return FindAddresses(PortType.None);
        }
    }
}
