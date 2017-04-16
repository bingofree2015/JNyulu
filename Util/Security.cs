using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace JNyuluSoft.Util
{
    ///<summary >
    /// MD5加密算法
    ///</summary> 
    public class Security
    {
        /// <summary>
        /// 16位或者32位MD5，小写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="code">16 or 32</param>
        /// <returns></returns>
        public static string MD5(string str, int code)
        {
            if (code == 16)
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
            }

            if (code == 32)
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
            }

            return "00000000000000000000000000000000";
        }

        /// <summary>
        /// 32位MD5，小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            return MD5(str, 32);
        }

        public static byte[] Md5EncodeByte(string text)
        {
            var md5 = new MD5Cng();

            var oldbytes = Encoding.UTF8.GetBytes(text);

            return md5.ComputeHash(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        /// 对字符串进行DES加密
        /// </summary>
        /// <param name="sourceString">待加密的字符串</param>
        /// <returns>加密后的BASE64编码的字符串</returns>
        public static string DESEncrypt(string sourceString, string key)
        {
            byte[] btKey = Encoding.Default.GetBytes(key);
            byte[] btIV = Encoding.Default.GetBytes(key);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Encoding.Default.GetBytes(sourceString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 对DES加密后的字符串进行解密
        /// </summary>
        /// <param name="encryptedString">待解密的字符串</param>
        /// <param name="key"></param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecrypt(string encryptedString, string key)
        {
            byte[] btKey = Encoding.Default.GetBytes(key);
            byte[] btIV = Encoding.Default.GetBytes(key);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Encoding.Default.GetString(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Encrypt(string str)
        {
            var result = string.Empty;

            try
            {
                byte[] b = System.Text.Encoding.UTF8.GetBytes(str);
                result = Convert.ToBase64String(b);
            }
            catch
            {
                new Exception("Base64编码异常");
            }

            return result;
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="base64str"></param>
        /// <returns></returns>
        public static string Base64Decrypt(string base64str)
        {
            if (string.IsNullOrEmpty(base64str))
                new Exception("参数不能为空！");

            var result = string.Empty;

            try
            {
                byte[] b = Convert.FromBase64String(base64str);
                result = System.Text.Encoding.UTF8.GetString(b);
            }
            catch
            {
                new Exception("Base64解码异常");
            }

            return result;
        }
    }
}
