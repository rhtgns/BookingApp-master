using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Entities
{
    //hangi odayı hangi müşteri rezerve etti?
    public class RezervationEntity : BaseEntity
    {
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GuestCount { get; set; }

        //relational property
        public UserEntity User { get; set; }
        public RoomEntity Room { get; set; }
    }
    public class RezervationConfiguration : BaseConfiguration<RezervationEntity>
    {
        public override void Configure(EntityTypeBuilder<RezervationEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
