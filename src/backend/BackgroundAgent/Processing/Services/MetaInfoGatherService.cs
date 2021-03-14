using BackgroundAgent.Processing.Logic;
using BackgroundAgent.Processing.Models;

using Serilog;

namespace BackgroundAgent.Processing.Services
{
    public class MetaInfoGatherService : IMetaInfoGatherService
    {
        public MetaInfoGatherService(IFileMetaInformationProvider metaInformationProvider)
        {
            _metaInformationProvider = metaInformationProvider;
        }
        
        public FileMetaInfo Gather(string path)
        {
            _logger.Information($"Gathering meta info for {path}");
            
            return _metaInformationProvider.GetInformation(path);
        }

        private readonly IFileMetaInformationProvider _metaInformationProvider;

        private readonly ILogger _logger = Log.ForContext<MetaInfoGatherService>();
    }
}