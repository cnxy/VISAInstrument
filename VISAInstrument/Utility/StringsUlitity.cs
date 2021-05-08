using System;
using System.Linq;

namespace VISAInstrument.Utility
{
    internal static class StringEx
    {
        public static bool TryParseByteStringToAsciiString(string byteString, out string asciiString)
        {
            asciiString = string.Empty;
            try
            {
                string[] byteStrings = byteString.Trim(' ').Split(' ');
                char[] charValues = byteStrings.Select(x => (char)Convert.ToByte(x, 16)).ToArray();
                asciiString = new string(charValues);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool TryParseAsciiStringToByteString(string asciiString, out string byteString)
        {
            byteString = string.Empty;
            try
            {
                byte[] byteValues = asciiString.Select(x => (byte)x).ToArray();
                byteString = BitConverter.ToString(byteValues).Replace("-", " ");
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool TryParseByteStringToByte(string byteString, out byte[] bytes)
        {
            bytes = null;
            try
            {
                string[] byteStrings = byteString.Trim(' ').Split(' ');
                bytes = byteStrings.Select(x => Convert.ToByte(x, 16)).ToArray();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
