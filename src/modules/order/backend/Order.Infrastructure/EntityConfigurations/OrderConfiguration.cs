using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;

namespace Order.Infrastructure;

public class OrderConfiguration : IEntityTypeConfiguration<Order.Domain.Order>
{
    public void Configure(EntityTypeBuilder<Order.Domain.Order> builder)
    {
        builder.Ignore(e => e.DomainEvents);
    }
}