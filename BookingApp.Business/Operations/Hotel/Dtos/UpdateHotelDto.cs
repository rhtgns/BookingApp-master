﻿using BookingApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.Hotel.Dtos
{
    public class UpdateHotelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Stars { get; set; }
        public string Location { get; set; }
        public AccomodationType AccomodationType { get; set; }
        public List<int> FeatureIds { get; set; }
    }
}
