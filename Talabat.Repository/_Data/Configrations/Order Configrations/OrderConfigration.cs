using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repositories._Data.Configrations.Order_Configrations
{
    internal class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress,
                shippingAddress => shippingAddress.WithOwner());

            builder.Property(O => O.Subtotal).HasColumnType("decimal(12,2)");

            builder.Property(O => O.Status)
                .HasConversion(
                status => status.ToString(),
                OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                );

            builder.HasMany(order => order.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
