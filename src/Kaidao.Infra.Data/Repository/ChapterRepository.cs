using Kaidao.Domain.AppEntity;
using Kaidao.Domain.Interfaces;
using Kaidao.Domain.Specifications;
using Kaidao.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Kaidao.Infra.Data.Repository
{
    public class ChapterRepository : Repository<Chapter>, IChapterRepository
    {
        public ChapterRepository(AppDbContext context)
            : base(context)
        {
        }

        public override Chapter GetById(Guid id)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.Id == id);
        }

        public Chapter GetChapterByBookIdAndOrder(Guid bookId, int order)
        {
            return DbSet.AsNoTracking().FirstOrDefault(c => c.BookId == bookId && c.Order == order);
        }

        public IQueryable<Chapter> GetChapterListByBookId(Guid bookId)
        {
            return (IQueryable<Chapter>)DbSet
                .AsNoTracking()
                .Where(c => c.BookId == bookId)
                .OrderBy(c => c.Order)
                .Select(p => new Chapter()
                {
                    Id = p.Id,
                    BookId = p.BookId,
                    Order = p.Order,
                    Name = p.Name,
                    Url = p.Url
                });
        }

        public Chapter GetLastChapterByBookId(Guid bookId)
        {
            return DbSet.AsNoTracking()
                .OrderByDescending(c => c.Order)
                .Select(p => new Chapter()
                {
                    Id = p.Id,
                    BookId = p.BookId,
                    Order = p.Order,
                    Name = p.Name,
                    Url = p.Url
                })
                .FirstOrDefault(c => c.BookId == bookId);
        }

        public SpecificationResponse<Chapter> GetPagination(ISpecification<Chapter> spec)
        {
            var specificationResponse = SpecificationEvaluator<Chapter>.GetQuery(DbSet.AsQueryable(), spec);

            specificationResponse.Queryable = specificationResponse.Queryable.Select(p => new Chapter()
            {
                Id = p.Id,
                BookId = p.BookId,
                Order = p.Order,
                Name = p.Name,
                Url = p.Url
            });

            return specificationResponse;
        }
    }
}