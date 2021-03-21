using System.IO;

using BackgroundAgent.Constants;
using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Services;

using EnsureThat;

using Serilog;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class EncryptionProcessor : BasicProcessor
    {
        public EncryptionProcessor(IAsymmetricalCryptoService rsaCryptoService,
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

            var encryptionKey = _rsaCryptoService.Decrypt(File.ReadAllBytes(FsLocation.ApplicationEncryptionKey));
            var iv = _rsaCryptoService.Decrypt(File.ReadAllBytes(FsLocation.ApplicationEncryptionIv));

            var content = File.ReadAllBytes(snapshot.Compressed ? snapshot.CompressedPath : snapshot.Path);
            var encryptedContent = _aesCryptoService.Encrypt(encryptionKey, iv, content);

            var encryptedFileLocation =
                Path.Combine(FsLocation.ApplicationTempData, snapshot.BaseFileName + ".encrypted");

            File.WriteAllBytes(encryptedFileLocation, encryptedContent);
            
            if (snapshot.Compressed && File.Exists(snapshot.CompressedPath))
                File.Delete(snapshot.CompressedPath);
            
            snapshot.EncryptedPath = encryptedFileLocation;
            Successor?.Process(snapshot);
        }

        private readonly IAsymmetricalCryptoService _rsaCryptoService;
        private readonly ISymmentricalCryptoService _aesCryptoService;

        private readonly ILogger _logger = Log.ForContext<EncryptionProcessor>();
    }
}