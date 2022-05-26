using AutoMapper;
using Kaidao.Application.ViewModels;
using Kaidao.Domain.AppEntity;
using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<AppUser, UserViewModel>();

            CreateMap<Author, AuthorViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Book, BookViewModel>();
            CreateMap<Chapter, ChapterViewModel>();
            CreateMap<AppRole, RoleViewModel>();
            CreateMap<Function, FunctionViewModel>();
            CreateMap<CommandInFunction, CommandInFunctionViewModel>();
        }
    }
}
