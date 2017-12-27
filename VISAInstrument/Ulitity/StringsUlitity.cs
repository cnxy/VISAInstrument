using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VISAInstrument.Ulitity
{
    static class StringsEx
    {
        public static string[] ToHexString(this string[] asciiCommands)
        {
            if (asciiCommands.Any(m => m.Any(n => n < 0 || n > 255)))
            {
                throw new ArgumentException("不是Ascii字符！");
            }
            return (from m in (from n in asciiCommands select Encoding.ASCII.GetBytes(n)) select BitConverter.ToString(m).Replace("-", "")).ToArray();
        }

        public static string ToHexString(this string asciiCommand)
        {
            return new string[] { asciiCommand }.ToHexString()[0];
        }

        public static string[] ToAsciiString(this string[] hexCommands)
        {
            if (hexCommands.Select(x => x.ToUpper()).Any(m => m.Any(n => (n < 48 || n > 57) && (n < 65 || n > 70))))
            {
                throw new ArgumentException("仅能包含十六进制字符！");
            }

            if(hexCommands.Any(m=> m.Length%2!=0))
            {
                throw new ArgumentException("Hex个数必须是2的倍数！");
            }

            string[] resultCommands = new string[hexCommands.Length];
            List<byte> list = null;
            char[] temp = null;
            for (int i = 0; i < hexCommands.Length; i++)
            {
                list = new List<byte>();
                for (int j = 0; j < hexCommands[i].Length; j = j + 2)
                {
                    temp = new char[2];
                    temp[0] = hexCommands[i][j];
                    temp[1] = hexCommands[i][j + 1];
                    list.Add(Convert.ToByte(new string(temp), 16));
                }
                resultCommands[i] = Encoding.ASCII.GetString(list.ToArray());
            }
            return resultCommands;
        }
        public static string ToAsciiString(this string hexCommand)
        {
            return new string[] { hexCommand }.ToAsciiString()[0];
        }
    }
}
