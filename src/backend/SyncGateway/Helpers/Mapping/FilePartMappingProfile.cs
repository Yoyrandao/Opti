using AutoMapper;

using CommonTypes.Contracts;

using DataAccess.Domain;

using SyncGateway.Contracts.Out;

namespace SyncGateway.Helpers.Mapping
{
    public class FilePartMappingProfile : Profile
    {
        public FilePartMappingProfile()
        {
            CreateMap<Change, FilePart>()
               .ForMember(x => x.Folder, opt => opt.Ignore());

            CreateMap<FilePart, FileState>();
        }
    }
}