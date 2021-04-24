using System;
using System.IO;
using System.Linq;
using System.Text;

using BackgroundAgent.Processing.Models;
using BackgroundAgent.Requests;

using CommonTypes.Contracts;

using EnsureThat;

using RestSharp;

using Utils.Http;

namespace BackgroundAgent.Processing.Tasks.Processors
{
    public class SendDataProcessor : BasicProcessor
    {
        public SendDataProcessor(IRequestFactory requestFactory, IRestClientFactory restClientFactory)
        {
            _requestFactory = requestFactory;
            _client = restClientFactory.Create();
        }
        
        public override void Process(object contract)
        {
            var snapshot = contract as FileSnapshot;
            EnsureArg.IsNotNull(snapshot);

            var changeSet = new ChangeSet
            {
                Identity = "aaron",
                Records = snapshot.Parts.Select(x =>
                {
                    var partContent = File.ReadAllText(x.Path);

                    return new Change
                    {
                        IsFirst = x.IsFirst,
                        PartName = x.PartName,
                        Compressed = snapshot.Compressed,
                        EncryptionHash = x.EncryptionHash,
                        CompressionHash = x.CompressionHash,
                        BaseFileName = snapshot.BaseFileName,
                        Base64Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(partContent))
                    };
                }).ToList()
            };

            var request = _requestFactory.CreateChangeSetSendingRequest(changeSet);
            _client.Execute(request);
        }

        private readonly IRestClient _client;
        private readonly IRequestFactory _requestFactory;
    }
}