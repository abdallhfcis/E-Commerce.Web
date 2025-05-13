using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Presistence.Data.Configurations
{
    internal class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods")
                   .Property(D => D.Price)
                   .HasColumnType("decimal(8,2)");

            builder.Property(D => D.ShortName)
                .HasColumnType("varchar(50)");

            builder.Property(D => D.Description)
                .HasColumnType("varchar(100)");

            builder.Property(D => D.DeliveryTime)
                .HasColumnType("varchar(50)");
        }
    }
}
