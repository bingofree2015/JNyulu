using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace JNyuluSoft.Util
{
    public class ShortFileNameUtil
    {
        static char[] HexDigits = new Char[]{ '0', '1', '2', '3', '4', '5', '6', '7', '8',
			'9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 
            'p', 'k', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        public static String ShortFileName(String encypStr)
        {

            MD5CryptoServiceProvider _md5Cryp = new MD5CryptoServiceProvider();
            byte[] _encypByte = Encoding.UTF8.GetBytes(encypStr);
            _encypByte = _md5Cryp.ComputeHash(_encypByte);

            String _hexString = BufferToHex(_encypByte);
            String _shortFileName = string.Empty;

            for (int i = 0; i < 8; i++)
            {
                String tmpString = _hexString.Substring(i * 4, 4);

                long hexLong = 0x3FFFFFFF & long.Parse(tmpString, NumberStyles.HexNumber);

                _shortFileName += HexDigits[int.Parse((hexLong % 34) + "")] + "";
            }
            return _shortFileName;
        }

        private static String BufferToHex(byte[] bytes)
        {
            return BufferToHex(bytes, 0, bytes.Length);
        }

        private static String BufferToHex(byte[] bytes, int m, int n)
        {
            StringBuilder _sb = new StringBuilder(2 * n);
            int k = m + n;
            for (int l = m; l < k; l++)
            {
                AppendHexPair(bytes[l], _sb);
            }
            return _sb.ToString();
        }

        private static void AppendHexPair(byte b, StringBuilder sb)
        {
            char _c0 = HexDigits[(b & 0xf0) >> 4];
            char _c1 = HexDigits[b & 0xf];
            sb.Append(_c0);
            sb.Append(_c1);
        }
    }
}
