using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes.Order_Module;

namespace Talabat.Repository.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");

            builder.Property(O=>O.Status)
                //  ف لداتابيز  string ل  Enum عشان يحول من 
                // Enum ولما يرجع يرجع 
                .HasConversion(OS=> OS.ToString() , OS=>(OrderStatus) Enum.Parse(typeof(OrderStatus), OS));
            // Address ل ال  DataBase في ال Mapping عشان مايعملش 
            builder.OwnsOne(O => O.ShippingAddress, OS => OS.WithOwner());

            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
