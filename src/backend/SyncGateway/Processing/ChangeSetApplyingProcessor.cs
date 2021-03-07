using System;
using System.IO;

using AutoMapper;

using DataAccess.Domain;
using DataAccess.Helpers;
using DataAccess.Repositories;

using EnsureThat;

using FtpDataAccess.Repositories;

using Serilog;

using SyncGateway.Contracts.Common;

using Utils.Retrying;

namespace SyncGateway.Processing
{
    public class ChangeSetApplyingProcessor : BasicProcessor
    {
        public ChangeSetApplyingProcessor(IFilePartRepository  filePartRepository,
                                          IFolderRepository    folderRepository,
                                          IRepeater<Exception> repeater,
                                          ITransactionFactory  transactionFactory,
                                          IMapper              mapper)
        {
            _mapper = mapper;
            _repeater = repeater;
            _folderRepository = folderRepository;
            _filePartRepository = filePartRepository;
            _transactionFactory = transactionFactory;
        }

        #region Implementation of BasicProcessor

        public override void Process(object contract)
        {
            var data = contract as ChangeSet;
            EnsureArg.IsNotNull(data);

            _logger.Information($"Executing ChangeSetApplyingProcessor ({data.Identity}).");

            foreach (var record in data.Records)
            {
                _repeater.Process(() =>
                {
                    var transaction = _transactionFactory.BeginTransaction();
                    using var memoryStream = new MemoryStream(Convert.FromBase64String(record.Base64Content));

                    var existingRecord = _filePartRepository.GetByPartName(record.PartName);

                    if (existingRecord == null)
                    {
                        if (record.IsFirst)
                        {
                            _filePartRepository.Add(_mapper.Map<FilePart>(record,
                                opt => opt.AfterMap((src, dest) => dest.Folder = data.Identity)));
                        }
                        else
                        {
                            _filePartRepository.AppendToFile(_mapper.Map<FilePart>(record,
                                opt => opt.AfterMap((src, dest) => dest.Folder = data.Identity)));
                        }
                    }
                    else
                    {
                        _folderRepository.RemoveFile(record.PartName, data.Identity);
                        _filePartRepository.UpdateFilePart(record.PartName, record.Hash);
                    }

                    _folderRepository.UploadFile(memoryStream, record.PartName, data.Identity);
                    transaction.Commit();
                });
            }
        }

        #endregion

        private readonly IFilePartRepository _filePartRepository;
        private readonly IFolderRepository _folderRepository;

        private readonly IRepeater<Exception> _repeater;
        private readonly ITransactionFactory _transactionFactory;
        private readonly IMapper _mapper;

        private readonly ILogger _logger = Log.ForContext<ChangeSetApplyingProcessor>();
    }
}