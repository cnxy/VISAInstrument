using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VISAInstrument.Port
{
    /// <summary>
    /// 所操作的端口类型
    /// </summary>
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
            result = VISA32.viFindRsrc(sesn, $"{ToStringFromPortType(portType)}?*", out int vi, out int retCount, desc);
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
            return list.ToArray();
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
