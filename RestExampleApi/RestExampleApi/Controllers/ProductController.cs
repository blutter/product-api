using RestExampleApi.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace RestExampleApi.Controllers
{
    public class ProductController : ApiController
    {
        // GET products
        public IEnumerable<ProductResponse> Get()
        {
            throw new NotImplementedException();
        }

        // GET products/{id}
        public ProductResponse Get(string id)
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
