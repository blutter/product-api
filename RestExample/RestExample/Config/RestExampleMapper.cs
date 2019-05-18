using AutoMapper;
using RestExample.Contracts;
using RestExample.Model;

namespace RestExample.Config
{
    public class RestExampleMapper : Profile
    {
        public RestExampleMapper()
        {
            CreateMap<ProductEntity, Product>();
            CreateMap<ProductFilter, ProductRepositoryFilter>();
        }
    }
}
