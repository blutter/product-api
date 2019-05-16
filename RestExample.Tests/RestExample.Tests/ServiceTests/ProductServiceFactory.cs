using AutoMapper;
using Moq;
using RestExample.Config;
using RestExample.Contracts;
using RestExample.Services;

namespace RestExample.Tests.ServiceTests
{
    public class ProductServiceFactory
    {
        public Mock<IProductRepository> ProductRepository { get; }

        private readonly Mapper _mapper;

        public ProductServiceFactory()
        {
            ProductRepository = new Mock<IProductRepository>();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(typeof(RestExampleMapper)));
            _mapper = new Mapper(mapperConfig);
        }

        public IProductService BuildProductService()
        {
            return new ProductService(_mapper, ProductRepository.Object);
        }
    }
}
