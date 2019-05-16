using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestExampleApi;
using RestExampleApi.Controllers;
using RestExampleApi.Models;

namespace RestExampleApi.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Arrange
            var controller = new ProductsController();

            // Act
            IEnumerable<ProductResponse> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            var controller = new ProductsController();

            // Act
            var result = controller.Get("5");

            // Assert
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            var controller = new ProductsController();

            // Act
            controller.Post(null);

            // Assert
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            var controller = new ProductsController();

            // Act
            controller.Put("5", null);

            // Assert
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            var controller = new ProductsController();

            // Act
            controller.Delete("5");

            // Assert
        }
    }
}
