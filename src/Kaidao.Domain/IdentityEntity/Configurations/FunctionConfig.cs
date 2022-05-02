using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaidao.Domain.IdentityEntity.Configurations
{
    public class FunctionConfig : IEntityTypeConfiguration<Function>
    {
        public void Configure(EntityTypeBuilder<Function> builder)
        {
            builder.ToTable("Functions");

            builder.HasKey(x => x.Id);

            builder.HasOne<Function>(f => f.Parent)
                .WithMany(f => f.Functions)
                .HasForeignKey(f => f.ParentId);

            builder.Property(x => x.Id)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(x => x.ParentId)
                .IsRequired(false)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}