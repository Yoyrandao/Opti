using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

using BackgroundAgent.Constants;
using BackgroundAgent.Processing.EventHandling;

using Microsoft.Extensions.Hosting;

using Serilog;

namespace BackgroundAgent.Workers
{
    public class InitializationWorker : BackgroundService
    {
        public InitializationWorker(
            FileSystemWatcher     fsWatcher,
            IFsChangeEventHandler changeEventHandler,
            IFsCreateEventHandler createEventHandler,
            IFsDeleteEventHandler deleteEventHandler)
        {
            _fsWatcher = fsWatcher;
            _changeEventHandler = changeEventHandler;
            _createEventHandler = createEventHandler;
            _deleteEventHandler = deleteEventHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            InitializeApplicationStructure();
            InitializeKeys();
            SubscribeToFilesystemEvents(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                InitializeApplicationStructure();

                await Task.Delay(10000, stoppingToken);
            }
        }

        private void InitializeApplicationStructure()
        {
            if (Directory.Exists(FsLocation.ApplicationRoot))
                return;

            _logger.Information($"Creating application folder ({FsLocation.ApplicationRoot})");

            Directory.CreateDirectory(FsLocation.ApplicationRoot);
            Directory.CreateDirectory(FsLocation.ApplicationData);
            Directory.CreateDirectory(FsLocation.ApplicationTempData);
        }

        private void SubscribeToFilesystemEvents(CancellationToken token)
        {
            _fsWatcher.Path = FsLocation.ApplicationData;

            _changeEventHandler.Prepare(token);
            _createEventHandler.Prepare(token);
            _deleteEventHandler.Prepare(token);

            _fsWatcher.Changed += _changeEventHandler.OnChanged;
            _fsWatcher.Created += _createEventHandler.OnCreated;
            _fsWatcher.Deleted += _deleteEventHandler.OnDeleted;

            _fsWatcher.EnableRaisingEvents = true;
            _fsWatcher.IncludeSubdirectories = true;
        }

        private static void InitializeKeys()
        {
            if (File.Exists(FsLocation.ApplicationPrivateKey) && File.Exists(FsLocation.ApplicationEncryptionKey))
                return;

            var rsaCryptoProvider = new RSACryptoServiceProvider();

            var aesCryptoProvider = new AesCryptoServiceProvider
            {
                Mode = CipherMode.CBC, KeySize = 128, BlockSize = 128
            };

            aesCryptoProvider.GenerateKey();

            var symmetricKey = aesCryptoProvider.Key;
            var iv = aesCryptoProvider.IV;
            var rsaPrivateKey = rsaCryptoProvider.ExportRSAPrivateKey();

            var encryptedSymmetricKey = rsaCryptoProvider.Encrypt(symmetricKey, RSAEncryptionPadding.Pkcs1);
            var encryptedIv = rsaCryptoProvider.Encrypt(iv, RSAEncryptionPadding.Pkcs1);

            File.WriteAllBytes(FsLocation.ApplicationEncryptionKey, encryptedSymmetricKey);
            File.WriteAllBytes(FsLocation.ApplicationEncryptionIv, encryptedIv);
            File.WriteAllBytes(FsLocation.ApplicationPrivateKey, rsaPrivateKey);
        }
        
        private readonly IFsChangeEventHandler _changeEventHandler;
        private readonly IFsCreateEventHandler _createEventHandler;
        private readonly IFsDeleteEventHandler _deleteEventHandler;

        private readonly FileSystemWatcher _fsWatcher;

        private readonly ILogger _logger = Log.ForContext<InitializationWorker>();
    }
}