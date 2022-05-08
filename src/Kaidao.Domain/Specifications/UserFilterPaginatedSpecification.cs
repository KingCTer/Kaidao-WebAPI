using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Domain.Specifications
{
    public class UserFilterPaginatedSpecification : BaseSpecification<AppUser>
    {
        public UserFilterPaginatedSpecification(int skip, int take, string query)
            : base(i => true)
        {
            ApplyPaging(skip, take);
        }
    }
}
