using AutoMapper;
using Moq;
using RestExample.Config;
using RestExample.Contracts;
using RestExampleApi.Controllers;

namespace RestExampleApi.Tests.Controllers
{
    public class ProductsControllerFactory
    {
        public Mock<IProductService> ProductService { get; }

        private readonly Mapper _mapper;

        public ProductsControllerFactory()
        {
            ProductService = new Mock<IProductService>();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(typeof(RestExampleApiMapper)));
            _mapper = new Mapper(mapperConfig);
        }

        public ProductsController BuildProductController()
        {
            return new ProductsController(_mapper, ProductService.Object);
        }
    }
}
