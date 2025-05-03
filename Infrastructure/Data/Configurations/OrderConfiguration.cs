

using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.Status).HasConversion(
                o => o.ToString(),
                o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
                );

            builder
                  .HasMany(o => o.OrderItems)
                  .WithOne()
                  .OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(o => o.Address).IsRequired();
        }
    }
}
