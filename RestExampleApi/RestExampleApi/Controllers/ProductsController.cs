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
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductsController(IMapper mapper, IProductService productService)
        {
            _mapper = mapper;
            _productService = productService;
        }

        // GET products
        public async Task<IHttpActionResult> Get([FromUri]ProductFilterQuery productFilterQuery)
        {
            var productFilter = _mapper.Map<ProductFilter>(productFilterQuery);
            var productList = await _productService.GetAllProducts(productFilter).ConfigureAwait(false);

            return Ok(_mapper.Map<IEnumerable<ProductResponse>>(productList));
        }

        // GET products/{id}
        public async Task<IHttpActionResult> Get(string id)
        {
            var product = await _productService.GetProduct(id).ConfigureAwait(false);

            if (product != null)
            {
                var response = _mapper.Map<ProductResponse>(product);
                return Ok(response);
            }
            else
            {
                return NotFound();
            }
        }

        // POST products
        public async Task<IHttpActionResult> Post([FromBody]ProductPostRequest productPostRequest)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productPostRequest);

                var id = await _productService.CreateProduct(product).ConfigureAwait(false);

                return Ok(new ProductPostResponse { Id = id });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT products/{id}
        public async Task<IHttpActionResult> Put(string id, [FromBody]ProductPutRequest productPutRequest)
        {
            try
            {
                var product = _mapper.Map<Product>(productPutRequest);

                product.Id = id;

                await _productService.UpdateProduct(product).ConfigureAwait(false);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // DELETE products/{id}
        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                await _productService.DeleteProduct(id).ConfigureAwait(false);

                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
