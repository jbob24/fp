using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Domain.Services
{
    public class Hasher
    {
        public static string Hash(string text)
        {
            var hasher = new SHA1CryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(text);
            var hash = hasher.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
