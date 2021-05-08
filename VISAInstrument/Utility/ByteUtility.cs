using System;

namespace VISAInstrument.Utility
{
    internal static class ByteEx
    {
        public static bool TryParseByteToByteString(byte[] bytes, out string byteString)
        {
            byteString = string.Empty;
            try
            {
                byteString = BitConverter.ToString(bytes).Replace("-", " ");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
