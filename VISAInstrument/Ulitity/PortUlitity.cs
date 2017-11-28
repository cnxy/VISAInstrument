using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

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
            List<string> list = new List<string>();
            int result = VISA32.viOpenDefaultRM(out int sesn);
            StringBuilder desc = new StringBuilder();
            result = VISA32.viFindRsrc(sesn, $"{ToStringFromPortType(portType)}?*INSTR", out int vi, out int retCount, desc);
            ThrowIfResultExcepiton(result);
            for (int i = 0; i < retCount; i++)
            {
                list.Add(desc.ToString());
                if (i != retCount - 1)
                {
                    result = VISA32.viFindNext(vi, desc);
                    ThrowIfResultExcepiton(result);
                }
            }
            return list.ToArray().Where(n=>!n.Contains("//")).ToArray();
        }

        public static string[] FindRS232Type(string[] addresses)
        {
            int result = VISA32.viOpenDefaultRM(out int sesn);
            List<string> list = new List<string>();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            StringBuilder sb3 = new StringBuilder();
            short intfType = 0;
            short intfNum = 0;
            for (int i = 0; i < addresses.Length; i++)
            {
                result = VISA32.viParseRsrcEx(sesn, addresses[i], ref intfType, ref intfNum, sb1, sb2, sb3);
                list.Add(sb3.ToString());
            }
            return list.ToArray();
        }

        public static bool OpenIPAddress(string address,out string fullAddress)
        {
            string addressTemp = $"TCPIP0::{address}::INSTR";
            fullAddress = addressTemp;
            int result = VISA32.viOpenDefaultRM(out int sesn);
            result = VISA32.viOpen(sesn, addressTemp, 0,2000,out int vi);
            VISA32.viClose(vi);
            return result == 0;
        }

        public static string[] FindAddresses()
        {
           return FindAddresses(PortType.None);
        }

        public static void ThrowIfResultExcepiton(int result)
        {
            if (result != 0 && result !=VISA32.VI_ERROR_RSRC_NFOUND)
                throw new ResultException($"无效的结果编号：{result}");
        }
    }

    class ResultException : Exception
    {
        public ResultException(string message) : base(message) { }
    }
}
