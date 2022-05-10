using Kaidao.Domain.AppEntity;

namespace Kaidao.Domain.Specifications
{
    public class BookFilterPaginatedSpecification : BaseSpecification<Book>
    {
        public BookFilterPaginatedSpecification(int skip, int take, string query)
            : base(i => true)
        {
            //AddWhere(i => i.AuthorId == "34394");
            ApplyPaging(skip, take);
            AddInclude(x => x.Author);
            AddInclude(x => x.Category);

            if (query == "")
            {
                ApplyOrderBy(orderByExpression: x => x.Key);
                return;
            }
            if (!query.Contains("orderBy"))
            {
                ApplyOrderBy(orderByExpression: x => x.Key);
            }
            var queryList = query.Split(";");
            foreach (var item in queryList)
            {
                if (item.Contains("where:"))
                {
                    var where = item.Split("=");
                    if (where.Length == 2)
                    {
                        switch (where[0])
                        {
                            case "where:authorId":
                                Guid authorId;
                                if (Guid.TryParse(where[1], out authorId))
                                {
                                    AddWhere(x => x.AuthorId == authorId);
                                }
                                break;

                            case "where:authorName":
                                AddWhere(x => x.Author.Name == where[1]);
                                break;

                            case "where:categoryId":
                                Guid categoryId;
                                if (Guid.TryParse(where[1], out categoryId))
                                {
                                    AddWhere(x => x.CategoryId == categoryId);
                                }
                                break;

                            case "where:categoryName":
                                AddWhere(x => x.Category.Name == where[1]);
                                break;

                            case "where:keyword":
                                if (!string.IsNullOrEmpty(where[1]))
                                {
                                    AddWhere(x =>
                                        x.Name.Contains(where[1])
                                     || x.Status.Contains(where[1])
                                     || x.Author.Name.Contains(where[1])
                                     || x.Category.Name.Contains(where[1])
                                    );
                                }
                                break;

                            default:
                                break;
                        }
                    }
                    var whereDiff = item.Split("!");
                    if (whereDiff.Length == 2)
                    {
                        switch (whereDiff[0])
                        {
                            case "where:name":
                                AddWhere(x => x.Name != whereDiff[1]);
                                break;

                            default:
                                break;
                        }
                    }
                    continue;
                }
                if (item.Contains("orderBy:"))
                {
                    switch (item)
                    {
                        case "orderBy:view:desc":
                            ApplyOrderByDescending(orderByDescendingExpression: x => x.View);
                            break;

                        case "orderBy:view:asc":
                            ApplyOrderBy(orderByExpression: x => x.View);
                            break;

                        case "orderBy:like:desc":
                            ApplyOrderByDescending(orderByDescendingExpression: x => x.Like);
                            break;

                        case "orderBy:like:asc":
                            ApplyOrderBy(orderByExpression: x => x.Like);
                            break;

                        case "orderBy:key:desc":
                            ApplyOrderByDescending(orderByDescendingExpression: x => x.Key);
                            break;

                        case "orderBy:key:asc":
                            ApplyOrderBy(orderByExpression: x => x.Key);
                            break;

                        default:
                            ApplyOrderBy(orderByExpression: x => x.Key);
                            break;
                    }
                    continue;
                }
            }
        }
    }
}