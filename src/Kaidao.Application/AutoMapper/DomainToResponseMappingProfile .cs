using AutoMapper;
using Kaidao.Application.Responses;
using Kaidao.Domain.AppEntity;

namespace Kaidao.Application.AutoMapper
{
    public class DomainToResponseMappingProfile : Profile
    {
        public DomainToResponseMappingProfile()
        {
            CreateMap<Book, BookResponse>()
                .ConstructUsing(c => new BookResponse(c.Id, c.Name, c.Key, c.Cover, c.Status, c.View, c.Like, c.Author.Name, c.Category.Name, c.ChapterTotal, c.Intro));

            CreateMap<Book, BookResponse>()
                .ConstructUsing(c => new BookResponse(c.Id, c.Name, c.Cover, c.Status));

            CreateMap<Chapter, ChapterResponse>()
                .ConstructUsing(c => new ChapterResponse(c.Id, c.BookId, c.Order, c.Name, c.Url, c.Content));
        }
    }
}