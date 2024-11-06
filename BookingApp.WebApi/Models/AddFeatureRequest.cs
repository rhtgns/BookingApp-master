using System.ComponentModel.DataAnnotations;

namespace BookingApp.WebApi.Models
{
    public class AddFeatureRequest
    {
        [Required]
        public string Title { get; set; }
    }
}
