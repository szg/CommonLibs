using System;
using System.Security.Cryptography;
using System.Text;

namespace CommonLibs.Utilities
{
    /// <summary>
    /// 加密算法类
    /// </summary>
    public class SecretUtilities
    {
        ///<summary>
        /// MD5加密（大写）
        /// </summary>
        /// <returns></returns>
        public static string MD5Encrypt32X2(string input, Encoding encoding)
        {
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            byte[] s = md5.ComputeHash(encoding.GetBytes(input));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                //将 ToString("X") 改为 ToString("X2") 修复 首位为数字的时候丢失的问题（31位)
                pwd = pwd + s[i].ToString("X2");
            }
            return pwd;
        }

        ///<summary>
        /// MD5加密（大写）,编码方式为UTF-8
        /// </summary>
        /// <returns></returns>
        public static string MD5Encrypt32X2(string input)
        {
            return MD5Encrypt32X2(input, Encoding.UTF8);
        }

        ///<summary>
        /// MD5加密（小写）
        /// </summary>
        /// <returns></returns>
        public static string MD5Encrypt(string input, Encoding encoding)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                var b = md5.ComputeHash(encoding.GetBytes(input));
                string s = null;
                for (int i = 0; i <= b.Length - 1; i++)
                {
                    s += b[i].ToString("x").PadLeft(2, '0'); ;
                }
                return s;
            }
        }

        ///<summary>
        /// MD5加密（小写）,编码方式为UTF-8
        /// </summary>
        /// <returns></returns>
        public static string MD5Encrypt(string input)
        {
            return MD5Encrypt(input, Encoding.UTF8);
        }
    }
}