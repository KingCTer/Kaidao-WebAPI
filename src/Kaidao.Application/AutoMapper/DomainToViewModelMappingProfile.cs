using AutoMapper;
using Kaidao.Application.ViewModels;
using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<AppUser, UserViewModel>();
        }
    }
}
