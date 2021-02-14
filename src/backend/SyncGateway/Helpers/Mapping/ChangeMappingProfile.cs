using AutoMapper;

using DataAccess.Domain;

using SyncGateway.Contracts.Common;

namespace SyncGateway.Helpers.Mapping
{
    public class ChangeMappingProfile : Profile
    {
        public ChangeMappingProfile()
        {
            CreateMap<Change, FilePart>()
               .ForMember(x => x.Folder, opt => opt.Ignore());
        }
    }
}