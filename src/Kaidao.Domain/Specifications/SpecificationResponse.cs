namespace Kaidao.Domain.Specifications
{
    public class SpecificationResponse<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> Queryable { get; set; }
        public int TotalRecords { get; set; }

        public SpecificationResponse(IQueryable<TEntity> queryable, int totalRecords)
        {
            Queryable = queryable;
            TotalRecords = totalRecords;
        }

        public SpecificationResponse()
        {
        }
    }
}