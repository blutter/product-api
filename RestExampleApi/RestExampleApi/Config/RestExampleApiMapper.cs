using AutoMapper;
using RestExample.Contracts;
using RestExampleApi.Models;

namespace RestExample.Config
{
    public class RestExampleApiMapper : Profile
    {
        public RestExampleApiMapper()
        {
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
        }
    }
}
