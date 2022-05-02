using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaidao.Domain.IdentityEntity.Configurations
{
    public class UserPermissionConfig : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.ToTable("UserPermissions");

            builder.HasKey(x => new { x.UserId, x.FunctionId, x.CommandId });

            builder.HasOne<Command>(up => up.Command)
                .WithMany(c => c.UserPermissions)
                .HasForeignKey(p => p.CommandId);

            builder.HasOne<Function>(up => up.Function)
                .WithMany(f => f.UserPermissions)
                .HasForeignKey(p => p.FunctionId);

            builder.HasOne<AppUser>(up => up.User)
                .WithMany(r => r.UserPermissions)
                .HasForeignKey(cf => cf.FunctionId);

            builder.Property(p => p.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(p => p.FunctionId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(p => p.CommandId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(p => p.Allow)
                .HasDefaultValue(false);
        }
    }
}