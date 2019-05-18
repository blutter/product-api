using System.ComponentModel.DataAnnotations;

namespace RestExampleApi.Models
{
    public class ProductPostRequest
    {
        /// <summary>
        /// Description of product
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Product Model details
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Brand of product
        /// </summary>
        [Required]
        public string Brand { get; set; }
    }
}