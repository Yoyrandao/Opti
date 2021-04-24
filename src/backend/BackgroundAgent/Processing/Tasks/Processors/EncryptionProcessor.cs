using System.IO;

using BackgroundAgent.Constants;
using BackgroundAgent.Processing.Models;
using BackgroundAgent.Processing.Services;

using EnsureThat;

using Serilog;

using Utils.Hashing;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class EncryptionProcessor : BasicProcessor
    {
        public EncryptionProcessor(IAsymmetricalCryptoService rsaCryptoService,
                                   ISymmentricalCryptoService aesCryptoService, IHashProvider hashProvider)
        {
            _rsaCryptoService = rsaCryptoService;
            _aesCryptoService = aesCryptoService;
            _hashProvider = hashProvider;
        }

        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);
            
            _logger.Information($"Running decryption process for {snapshot.BaseFileName}");

            var encryptionKey = _rsaCryptoService.Decrypt(File.ReadAllBytes(FsLocation.ApplicationEncryptionKey));
            var iv = _rsaCryptoService.Decrypt(File.ReadAllBytes(FsLocation.ApplicationEncryptionIv));

            foreach (var part in snapshot.Parts)
            {
                var content = File.ReadAllBytes(part.Path);
                var encryptedContent = _aesCryptoService.Encrypt(encryptionKey, iv, content);
                
                var encryptedFileLocation =
                    Path.Combine(FsLocation.ApplicationTempData, part.Path + ".encrypted");
                
                File.WriteAllBytes(encryptedFileLocation, encryptedContent);
                File.Delete(part.Path);
                
                part.Path = encryptedFileLocation;
                part.EncryptionHash = _hashProvider.Hash(File.ReadAllText(encryptedFileLocation));
            }
            
            Successor?.Process(snapshot);
        }

        private readonly IAsymmetricalCryptoService _rsaCryptoService;
        private readonly ISymmentricalCryptoService _aesCryptoService;

        private readonly IHashProvider _hashProvider;

        private readonly ILogger _logger = Log.ForContext<EncryptionProcessor>();
    }
}