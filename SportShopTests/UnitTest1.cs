using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using SportShop.Controllers;
using SportShop.Models;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace SportShopTests
{
    public class UnitTest1
    {

        [Fact]
        public void IndexReturnsAllProducts()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "first"},
                new Product {ProductID = 2, Name = "second"},
                new Product {ProductID = 3, Name = "third"},
            }.AsQueryable);


            var controller = new AdminController(mock.Object);

            var result = controller.Index();

            var model = (IEnumerable<Product>)(result).Model;
            var array = model.ToArray();

            Assert.Equal(3, model.Count());
            Assert.Equal("first",array[0].Name);
            Assert.Equal("second",array[1].Name);
            Assert.Equal("third",array[2].Name);
        }
    }
}
