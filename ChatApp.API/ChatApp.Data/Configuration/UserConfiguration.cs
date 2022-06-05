using ChatApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChatApp.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.UserName)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(u => u.Name)
                   .HasMaxLength(100)
                   .IsRequired();
            builder.Property(u => u.Surname)
                   .HasMaxLength(100)
                   .HasDefaultValue("XXX");
            builder
              .HasMany(u => u.Messages)
              .WithOne(m => m.SendUser)
              .HasForeignKey(m => m.SendUserId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
