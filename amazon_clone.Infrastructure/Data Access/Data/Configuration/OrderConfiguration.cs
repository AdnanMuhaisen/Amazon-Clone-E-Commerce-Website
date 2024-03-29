﻿using amazon_clone.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace amazon_clone.DataAccess.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("tbl_Orders").HasKey(x => x.OrderID);

            builder.Property(x => x.OrderID).ValueGeneratedOnAdd();

            builder.OwnsOne(x => x.CreationDetails);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerID)
                .IsRequired(true);

            builder.HasOne(x => x.ShoppingCart)
                .WithOne(x => x.Order)
                .HasForeignKey<Order>(x => x.ShoppingCartID)
                .IsRequired(true);

            builder.HasOne(x => x.OrderStatus)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.StatusID)
                .IsRequired(true);

            builder.HasOne(x => x.ShippingDetails)
                .WithOne(x => x.Order)
                .HasForeignKey<Order>(x => x.ShippingDetailsID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
