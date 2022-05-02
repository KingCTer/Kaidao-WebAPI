using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaidao.Domain.IdentityEntity.Configurations
{
    public class CommandInFunctionConfig : IEntityTypeConfiguration<CommandInFunction>
    {
        public void Configure(EntityTypeBuilder<CommandInFunction> builder)
        {
            builder.ToTable("CommandInFunctions");

            builder.HasKey(cf => new { cf.CommandId, cf.FunctionId });

            builder.HasOne<Command>(cf => cf.Command)
                .WithMany(c => c.CommandInFunctions)
                .HasForeignKey(cf => cf.CommandId);

            builder.HasOne<Function>(cf => cf.Function)
                .WithMany(c => c.CommandInFunctions)
                .HasForeignKey(cf => cf.FunctionId);


            builder.Property(x => x.CommandId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(x => x.FunctionId)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(128)
                .IsUnicode();
        }
    }
}