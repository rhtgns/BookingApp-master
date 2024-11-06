using BookingApp.Business.Operations.Hotel.Dtos;
using BookingApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Business.Operations.Hotel
{
    public interface IHotelService
    {
        Task<ServiceMessage> AddHotel(AddHotelDto hotel);
        Task<HotelDto> GetHotel(int id);
        Task<List<HotelDto>> GetAllHotels();
        Task<ServiceMessage> AdjustHotelStars(int id, int changeBy);
        Task<ServiceMessage> DeleteHotel(int id);
        Task<ServiceMessage> UpdateHotel(UpdateHotelDto hotel);
    }
}
