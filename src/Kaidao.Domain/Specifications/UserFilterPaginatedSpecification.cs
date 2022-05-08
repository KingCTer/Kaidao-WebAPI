using Kaidao.Domain.IdentityEntity;

namespace Kaidao.Domain.Specifications
{
    public class UserFilterPaginatedSpecification : BaseSpecification<AppUser>
    {
        public UserFilterPaginatedSpecification(int skip, int take, string query)
            : base(i => true)
        {
            ApplyPaging(skip, take);

            if (!string.IsNullOrEmpty(query))
            {
                AddWhere(x =>
                    x.UserName.Contains(query)
                 || x.PhoneNumber.Contains(query)
                 || x.FirstName.Contains(query)
                 || x.LastName.Contains(query)
                 || x.LastName.Contains(query)
                 || x.Email.Contains(query)
                );
            }
        }
    }
}
