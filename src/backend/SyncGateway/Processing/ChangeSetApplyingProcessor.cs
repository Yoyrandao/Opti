using System.IO;

using DataAccess.Repositories;

using EnsureThat;

using FtpDataAccess.Repositories;

using SyncGateway.Contracts.Common;

using Utils.Retrying;

namespace SyncGateway.Processing
{
    public class ChangeSetApplyingProcessor : BasicProcessor
    {
        public ChangeSetApplyingProcessor(IFilePartRepository filePartRepository, IFolderRepository folderRepository,
                                          IRepeater<FileNotFoundException> repeater)
        {
            _filePartRepository = filePartRepository;
            _folderRepository = folderRepository;
            _repeater = repeater;
        }

        #region Implementation of BasicProcessor

        public override void Process(object contract)
        {
            var data = contract as ChangeSet;
            EnsureArg.IsNotNull(data);

            foreach (var record in data.Records)
            {
                File.WriteAllText(record.PartName, record.Base64Content);

                _repeater.Process(() =>
                {
                    _folderRepository.UploadFile(".", record.PartName, data.Identity);
                    
                    /*
                     * TODO: File uploading logic
                     */
                });
            }
        }

        #endregion

        private readonly IFilePartRepository _filePartRepository;
        private readonly IFolderRepository _folderRepository;

        private readonly IRepeater<FileNotFoundException> _repeater;
    }
}