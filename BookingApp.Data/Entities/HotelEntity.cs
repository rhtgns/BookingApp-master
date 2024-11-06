﻿using BookingApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Entities
{
    public class HotelEntity : BaseEntity
    {
        public string Name { get; set; }
        public int? Stars { get; set; }
        public string Location { get; set; }
        public AccomodationType AccomodationType { get; set; }


        //relational Propertty
        public ICollection<HotelFeatureEntity> HotelFeatures { get; set; }
        public ICollection<RoomEntity> Rooms { get; set; }
    }
    public class HotelConfiguration : BaseConfiguration<HotelEntity>
    {
        public override void Configure(EntityTypeBuilder<HotelEntity> builder)
        {
            builder.Property(x => x.Stars).IsRequired(false);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(80);
            base.Configure(builder);
        }
    }
}
