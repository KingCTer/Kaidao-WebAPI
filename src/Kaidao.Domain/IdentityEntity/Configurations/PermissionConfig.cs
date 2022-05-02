using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaidao.Domain.IdentityEntity.Configurations
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(x => new { x.RoleId, x.FunctionId, x.CommandId });

            builder.HasOne<Command>(p => p.Command)
                .WithMany(c => c.Permissions)
                .HasForeignKey(p => p.CommandId);

            builder.HasOne<Function>(p => p.Function)
                .WithMany(f => f.Permissions)
                .HasForeignKey(p => p.FunctionId);

            builder.HasOne<AppRole>(p => p.Role)
                .WithMany(r => r.Permissions)
                .HasForeignKey(cf => cf.RoleId);

            builder.Property(p => p.RoleId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(p => p.FunctionId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(p => p.CommandId)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}