using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SFY_Word_Book.Shared.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// MD5加密（密码暗文）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string GetMD5(this string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentNullException(nameof(data));
            }
            var hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(data));
            return Convert.ToBase64String(hash);
        }
    }
}
