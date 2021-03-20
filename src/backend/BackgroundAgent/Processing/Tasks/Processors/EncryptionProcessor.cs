using System.IO;
using System.Security.Cryptography;

using BackgroundAgent.Constants;
using BackgroundAgent.Processing.Models;

using EnsureThat;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class EncryptionProcessor : BasicProcessor
    {
        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);

            var cryptoProvider = new RSACryptoServiceProvider();

            cryptoProvider.ImportRSAPrivateKey(File.ReadAllBytes(FsLocation.ApplicationPrivateKey), out _);
            var encryptedData = cryptoProvider.Encrypt(File.ReadAllBytes(snapshot.CompressedPath), RSAEncryptionPadding.Pkcs1);
            var encryptedFileLocation = Path.Combine(FsLocation.ApplicationTempData, snapshot.BaseFileName + ".encrypted");

            File.WriteAllBytes(encryptedFileLocation, encryptedData);

            snapshot.EncryptedPath = encryptedFileLocation;
            Successor?.Process(snapshot);
        }
    }
}