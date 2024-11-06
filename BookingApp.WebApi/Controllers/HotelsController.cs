using BookingApp.Business.Operations.Hotel;
using BookingApp.Business.Operations.Hotel.Dtos;
using BookingApp.WebApi.Filters;
using BookingApp.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpPost]
        public async Task<IActionResult> AddHotel([FromBody] AddHotelRequest request)
        {
            var addHotelDto = new AddHotelDto
            {
                Name = request.Name,
                Stars = request.Stars,
                Location = request.Location,
                AccomodationType = request.AccomodationType,
                FeaturesIds = request.FeaturesIds
            };

            var result = await _hotelService.AddHotel(addHotelDto);
            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotel(int id)
        {
            var hotel = await _hotelService.GetHotel(id);
            if(hotel is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(hotel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _hotelService.GetAllHotels();
            return Ok(hotels);
        }

        [HttpPatch("{id}/stars")]
        public async Task<IActionResult> AdjustHotelStars(int id,int changeBy)
        {
            var result = await _hotelService.AdjustHotelStars(id, changeBy);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            else
            {
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var result = await _hotelService.DeleteHotel(id);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            else
            {
                return Ok();
            }

        }

        [HttpPut("{id}")]
        [TimeControlFilter]
        public async Task<IActionResult> UpdateHotel(int id,UpdateHotelRequest request)
        {
            var updateHotelDto = new UpdateHotelDto
            {
                Id = id,
                Name = request.Name,
                Stars = request.Stars,
                Location = request.Location,
                AccomodationType = request.AccomodationType,
                FeatureIds = request.FeatureIds,
            };
            var result = await _hotelService.UpdateHotel(updateHotelDto);
            if (!result.IsSucceed)
            {
                return NotFound("Otel bulunamadı");
            }
            else
            {
                return await GetHotel(id);
            }
        }
    }
}
