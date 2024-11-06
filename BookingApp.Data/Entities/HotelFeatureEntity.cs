using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Data.Entities
{
    //Hangi otelde hangi özellik var ?
    public class HotelFeatureEntity : BaseEntity
    {
        public int HotelId { get; set; }
        public int FeatureId { get; set; }

        //relational Property
        public HotelEntity Hotel { get; set; }
        public FeatureEntity Feature { get; set; }
    }
    public class HotelFeatureConfiguration : BaseConfiguration<HotelFeatureEntity>
    {
        public override void Configure(EntityTypeBuilder<HotelFeatureEntity> builder)
        {
            builder.Ignore(x => x.Id); //ıd propertysini göçrmezden geldik tabloya aktarılmayacak
            builder.HasKey("HotelId", "FeatureId");
            //composite key oluşturup yeni primary key olarak atadık
            base.Configure(builder);
        }
    }
}
