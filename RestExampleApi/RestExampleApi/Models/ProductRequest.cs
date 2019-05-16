using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestExampleApi.Models
{
    public class ProductRequest
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
    }
}