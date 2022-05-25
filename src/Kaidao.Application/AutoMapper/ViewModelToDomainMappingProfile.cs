using AutoMapper;
using Kaidao.Application.Responses;
using Kaidao.Application.ViewModels;
using Kaidao.Domain.Commands.Author;
using Kaidao.Domain.Commands.Book;
using Kaidao.Domain.Commands.Category;
using Kaidao.Domain.Commands.Chapter;

namespace Kaidao.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<AuthorViewModel, RegisterNewAuthorCommand>()
                .ConstructUsing(c => new RegisterNewAuthorCommand(c.Name));

            CreateMap<CategoryViewModel, RegisterNewCategoryCommand>()
                .ConstructUsing(c => new RegisterNewCategoryCommand(c.Name));

            CreateMap<BookViewModel, RegisterNewBookCommand>()
                .ConstructUsing(c => new RegisterNewBookCommand(c.Name, c.Key, c.Cover, c.Status, c.View, c.Like, c.AuthorId, c.CategoryId, c.ChapterTotal, c.Intro));
            CreateMap<BookViewModel, UpdateBookCommand>()
                .ConstructUsing(c => new UpdateBookCommand(c.Id, c.Name, c.Key, c.Cover, c.Status, c.View, c.Like, c.AuthorId, c.CategoryId, c.ChapterTotal, c.Intro));

            CreateMap<ChapterViewModel, RegisterNewChapterCommand>()
                .ConstructUsing(c => new RegisterNewChapterCommand(c.BookId, c.Order, c.Name, c.Url, c.Content));
            CreateMap<ChapterViewModel, UpdateChapterCommand>()
                .ConstructUsing(c => new UpdateChapterCommand(c.Id, c.Order, c.Name, c.Url, c.Content, c.BookId));

            CreateMap<ChapterResponse, UpdateChapterCommand>()
               .ConstructUsing(c => new UpdateChapterCommand(c.Id, c.Order, c.Name, c.Url, c.Content, c.BookId));
        }
    }
}