namespace RestExampleApi.Models
{
    public class ProductPutRequest
    {
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