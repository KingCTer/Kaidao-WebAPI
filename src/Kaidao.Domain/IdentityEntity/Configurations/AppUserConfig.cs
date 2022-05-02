﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kaidao.Domain.IdentityEntity.Configurations
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");

            builder.Property(p => p.Id)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        }
    }
}