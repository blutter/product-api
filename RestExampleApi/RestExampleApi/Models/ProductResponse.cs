namespace RestExampleApi.Models
{
    public class ProductResponse
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Description of product
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Product Model details
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Brand of product
        /// </summary>
        public string Brand { get; set; }
    }
}