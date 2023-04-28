using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("int").UseIdentityColumn(seed: 1, increment: 1).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x=>x.Name).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            builder.Property(x=>x.CreatedDate).HasColumnType("datetime").IsRequired();
            builder.ToTable("Categories");
        }
    }
}
