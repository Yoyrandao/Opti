using System.IO;
using System.Security.Cryptography;

using BackgroundAgent.Constants;
using BackgroundAgent.Processing.Models;

using EnsureThat;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class DecryptionProcessor : BasicProcessor
    {
        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);

            var cryptoProvider = new RSACryptoServiceProvider();

            cryptoProvider.ImportRSAPrivateKey(File.ReadAllBytes(FsLocation.ApplicationPrivateKey), out _);

            var decryptedData =
                cryptoProvider.Decrypt(File.ReadAllBytes(snapshot.EncryptedPath), RSAEncryptionPadding.Pkcs1);

            var decryptedFileLocation =
                Path.Combine(FsLocation.ApplicationTempData, snapshot.DecryptedPath + ".decrypted");

            File.WriteAllBytes(decryptedFileLocation, decryptedData);

            snapshot.DecryptedPath = decryptedFileLocation;
            Successor?.Process(snapshot);
        }
    }
}