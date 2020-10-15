using DEM.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.EF
{
    public class DEMEntityConfiguration : IEntityTypeConfiguration<Payer>
    {
        public void Configure(EntityTypeBuilder<Payer> builder)
        {
            builder.HasData(
                new Payer()
                {
                    Id = Guid.NewGuid(),
                    Code = "VK",
                    Name = "Vợ",
                    CreatedBy="ADMIN",
                    CreatedDate = DateTime.Now
                },
                new Payer()
                {
                    Id = Guid.NewGuid(),
                    Code = "CK",
                    Name = "Chồng",
                    CreatedBy = "ADMIN",
                    CreatedDate = DateTime.Now
                }
            ); ;
        }
    }
}
