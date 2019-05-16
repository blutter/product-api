using AutoMapper;
using RestExample.Contracts;
using RestExampleApi.Models;

namespace RestExample.Config
{
    public class RestExampleApiMapper : Profile
    {
        public RestExampleApiMapper()
        {
            CreateMap<ProductPostRequest, Product>();
            CreateMap<Product, ProductResponse>();
        }
    }
}
