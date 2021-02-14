using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Utils.Hashing
{
    public class Sha256HashProvider : IHashProvider
    {
        #region Implementation of IHashProvider

        public string Hash(string content)
        {
            using var sha256Algo = SHA256.Create();

            var rawHashBuffer = sha256Algo.ComputeHash(Encoding.ASCII.GetBytes(content));

            return string.Join("", rawHashBuffer.Select(x => x.ToString("X2").ToLower()));
        }

        #endregion
    }
}
