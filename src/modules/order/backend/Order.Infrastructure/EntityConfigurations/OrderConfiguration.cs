using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain;

namespace Order.Infrastructure;

public class OrderConfiguration : IEntityTypeConfiguration<Order.Domain.Order>
{
    public void Configure(EntityTypeBuilder<Order.Domain.Order> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        builder.OwnsOne(x => x.ShipToAddress, a =>
        {
            a.Property(p => p.Country).HasColumnName("Country");
            a.Property(p => p.City).HasColumnName("City");
            a.Property(p => p.ZipCode).HasColumnName("ZipCode");
            a.Property(p => p.Street).HasColumnName("Street");
        });

        builder.OwnsOne(x => x.Status, a => 
        {   
            a.Property(p => p.Code).HasColumnName("Status");
            a.Ignore(b => b.Description);
        });
    }
}