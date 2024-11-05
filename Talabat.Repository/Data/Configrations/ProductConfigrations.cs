using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Configrations
{
    internal class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(P => P.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(P => P.Description)
                   .IsRequired();

            builder.Property(P => P.PictureUrl)
                   .IsRequired();

            builder.HasOne(P => P.Category)
                   .WithMany(C => C.Products);

            builder.HasOne(P => P.Brand)
                   .WithMany(B => B.Products);
        }
    }
}
