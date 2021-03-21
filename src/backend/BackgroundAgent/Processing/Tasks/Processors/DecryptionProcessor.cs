using System.IO;

using BackgroundAgent.Constants;
using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Services;

using EnsureThat;

using Serilog;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class DecryptionProcessor : BasicProcessor
    {
        public DecryptionProcessor(IAsymmetricalCryptoService rsaCryptoService,
                                   ISymmentricalCryptoService aesCryptoService)
        {
            _rsaCryptoService = rsaCryptoService;
            _aesCryptoService = aesCryptoService;
        }

        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);
            
            _logger.Information($"Running decryption process for {snapshot.BaseFileName}");

            var decryptionKey = _rsaCryptoService.Decrypt(File.ReadAllBytes(FsLocation.ApplicationEncryptionKey));
            var iv = _rsaCryptoService.Decrypt(File.ReadAllBytes(FsLocation.ApplicationEncryptionIv));

            var content = File.ReadAllBytes(snapshot.EncryptedPath);
            var decryptedContent = _aesCryptoService.Decrypt(decryptionKey, iv, content);

            var decryptedFileLocation =
                Path.Combine(FsLocation.ApplicationTempData, snapshot.BaseFileName + ".decrypted");

            File.WriteAllBytes(decryptedFileLocation, decryptedContent);

            snapshot.DecryptedPath = decryptedFileLocation;
            Successor?.Process(snapshot);
        }

        private readonly IAsymmetricalCryptoService _rsaCryptoService;
        private readonly ISymmentricalCryptoService _aesCryptoService;

        private readonly ILogger _logger = Log.ForContext<DecryptionProcessor>();
    }
}