using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repositories._Data.Configrations.Order_Configrations
{
    internal class OrderItemConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(orderItem => orderItem.Product, product => product.WithOwner());

            builder.Property(orderItem => orderItem.Price)
                .HasColumnType("decimal(12,2)");

        }
    }
}
