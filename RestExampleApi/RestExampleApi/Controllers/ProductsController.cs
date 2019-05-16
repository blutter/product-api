﻿using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        // GET products
        public IEnumerable<ProductResponse> Get()
        {
            throw new NotImplementedException();
        }

        // GET products/{id}
        public async Task<ProductResponse> Get(string id)
        {
            var product = await _productService.GetProduct(id).ConfigureAwait(false);

            return _mapper.Map<ProductResponse>(product);
        }

        // POST products
        public async Task<ProductPostResponse> Post([FromBody]ProductPostRequest productPostRequest)
        {
            var product = _mapper.Map<Product>(productPostRequest);

            var id = await _productService.CreateProduct(product).ConfigureAwait(false);

            return new ProductPostResponse { Id = id };
        }

        // PUT products/{id}
        public void Put(string id, [FromBody]ProductPostRequest product)
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
