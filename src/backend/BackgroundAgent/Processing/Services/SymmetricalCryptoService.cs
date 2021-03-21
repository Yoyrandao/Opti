using System.Security.Cryptography;

namespace BackgroundAgent.Processing.Services
{
    public class SymmetricalCryptoService : ISymmentricalCryptoService
    {
        public SymmetricalCryptoService()
        {
            _cryptoServiceProvider = new AesCryptoServiceProvider
            {
                Mode = CipherMode.CBC, KeySize = 128, BlockSize = 128
            };
        }
        
        public byte[] Encrypt(byte[] key, byte[] iv, byte[] content)
        {
            _cryptoServiceProvider.Key = key;
            _cryptoServiceProvider.IV = iv;

            var encryptor = _cryptoServiceProvider.CreateEncryptor();

            return encryptor.TransformFinalBlock(content, 0, content.Length);
        }

        public byte[] Decrypt(byte[] key, byte[] iv, byte[] content)
        {
            _cryptoServiceProvider.Key = key;
            _cryptoServiceProvider.IV = iv;

            var decryptor = _cryptoServiceProvider.CreateDecryptor();

            return decryptor.TransformFinalBlock(content, 0, content.Length);
        }

        private readonly AesCryptoServiceProvider _cryptoServiceProvider;
    }
}