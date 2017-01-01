/*
 * Author:ImL1s
 *
 * Date:2016/05/17
 *
 * Email:ImL1s@outlook.com
 *
 * description:MD5工具.
 */

using System.Security.Cryptography;
using System.Text;


namespace YSFramework.Tools
{

    public class MD5Tool
    {

        public static string GetMD5(int value)
        {
            string sValue = value.ToString();
            return GetMD5(sValue);
        }

        public static string GetMD5(string value)
        {
            MD5 md5 = MD5.Create();

            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
