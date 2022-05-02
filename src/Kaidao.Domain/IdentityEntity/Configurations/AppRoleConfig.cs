using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaidao.Domain.IdentityEntity.Configurations
{
    public class AppRoleConfig : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.ToTable("AppRoles");

            builder.Property(p => p.Id)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(128)
                .IsUnicode();
        }
    }
}