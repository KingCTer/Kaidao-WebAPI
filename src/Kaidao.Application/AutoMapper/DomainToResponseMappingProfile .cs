using AutoMapper;
using Kaidao.Application.Responses;
using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Application.AutoMapper
{
    public class DomainToResponseMappingProfile : Profile
    {
        public DomainToResponseMappingProfile()
        {
            CreateMap<AppUser, UserResponse>()
                .ConstructUsing(c => new UserResponse(c.Id, c.UserName, c.Email, c.PhoneNumber));
        }
    }
}
