using AutoMapper;
using RestExample.Contracts;
using RestExampleApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestExampleApi.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IMapper mapper;
        private readonly IProductService productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            this.mapper = mapper;
            this.productService = productService;
        }

        // GET products
        public IEnumerable<ProductResponse> Get()
        {
            throw new NotImplementedException();
        }

        // GET products/{id}
        public async Task<ProductResponse> Get(string id)
        {
            throw new NotImplementedException();
        }

        // POST products
        public void Post([FromBody]ProductRequest product)
        {
            throw new NotImplementedException();
        }

        // PUT products/{id}
        public void Put(string id, [FromBody]ProductRequest product)
        {
            throw new NotImplementedException();
        }

        // DELETE products/{id}
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
