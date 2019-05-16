using System.ComponentModel.DataAnnotations;

namespace RestExampleApi.Models
{
    public class ProductPostRequest
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Brand { get; set; }
    }
}