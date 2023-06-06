using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(x => x.Id).HasColumnType("int").UseIdentityColumn(1, 1).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.FirstName).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.LastName).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.Email).HasColumnType("nvarchar(50)").IsRequired();
            builder.HasIndex(x=>x.Email).IsUnique();
            builder.Property(x => x.PasswordSalt).HasColumnType("varbinary(500)").IsRequired();
            builder.Property(x => x.PasswordHash).HasColumnType("varbinary(500)").IsRequired();
        }
    }
}
