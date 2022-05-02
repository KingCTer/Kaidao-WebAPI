using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaidao.Domain.IdentityEntity.Configurations
{
    public class CommandConfig : IEntityTypeConfiguration<Command>
    {
        public void Configure(EntityTypeBuilder<Command> builder)
        {
            builder.ToTable("Commands");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128)
                .IsUnicode();


        }
    }
}