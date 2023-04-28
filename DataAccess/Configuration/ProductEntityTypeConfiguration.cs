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
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("int").UseIdentityColumn(seed: 1, increment: 1).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            builder.Property(x=>x.ShopFavorites).HasColumnType("bit").IsRequired();
            builder.Property(x => x.ImageUrl).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(false);
            builder.Property(x=>x.CategoryId).HasColumnType("int").IsRequired();
            builder.ToTable("Products");

            builder.HasOne(x=>x.Category).WithMany(p=>p.Products).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
