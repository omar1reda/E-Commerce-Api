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
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(p => p.Cost).HasColumnType("decimal(18,2)");
            builder.Property(p => p.ShortName).IsRequired();
            builder.Property(p=>p.Descreption).IsRequired();
        }
    }
}
