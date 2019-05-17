using AutoMapper;
using RestExample.Contracts;
using RestExampleApi.Models;

namespace RestExample.Config
{
    public class RestExampleApiMapper : Profile
    {
        public RestExampleApiMapper()
        {
            CreateMap<ProductPostRequest, Product>().ForMember(product => product.Id, option => option.Ignore());
            CreateMap<ProductPutRequest, Product>().ForMember(product => product.Id, option => option.Ignore());
            CreateMap<Product, ProductResponse>();
        }
    }
}
