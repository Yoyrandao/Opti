using System;
using System.IO;
using System.Security.Cryptography;

using BackgroundAgent.Constants;

namespace BackgroundAgent.Processing.Services
{
    public class AssymentricalCryptoService : IAsymmetricalCryptoService
    {
        public AssymentricalCryptoService()
        {
            _cryptoServiceProvider = new RSACryptoServiceProvider();
        }

        public byte[] Encrypt(byte[] content)
        {
            _cryptoServiceProvider.ImportRSAPrivateKey(File.ReadAllBytes(FsLocation.ApplicationPrivateKey),
                out var bytesRead);

            if (bytesRead == 0)
                throw new Exception("Keyset does not exists.");

            return _cryptoServiceProvider.Encrypt(content, RSAEncryptionPadding.Pkcs1);
        }

        public byte[] Decrypt(byte[] content)
        {
            _cryptoServiceProvider.ImportRSAPrivateKey(File.ReadAllBytes(FsLocation.ApplicationPrivateKey),
                out var bytesRead);

            if (bytesRead == 0)
                throw new Exception("Keyset does not exists.");

            return _cryptoServiceProvider.Decrypt(content, RSAEncryptionPadding.Pkcs1);
        }

        private readonly RSACryptoServiceProvider _cryptoServiceProvider;
    }
}