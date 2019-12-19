using System;
using System.Collections;
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
    public class ProductControllerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ReturningProductById_ShouldReturnSingleProduct(int value)
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "first", CategoryID = 1},
                new Product {ProductID = 2, Name = "second", CategoryID = 2},
                new Product {ProductID = 3, Name = "third", CategoryID = 3},
            }.AsQueryable);

            var controller = new ProductController(mock.Object);
            var result = controller.ProductId(value);
            var model = (IEnumerable<Product>) result.Model;
            Assert.Single(model);
        }
        
        [Fact]
        public void ReturningProductByCategory_ShouldReturn2()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "first", CategoryID = 1},
                new Product {ProductID = 1, Name = "first2", CategoryID = 1},
                new Product {ProductID = 2, Name = "second", CategoryID = 2},
                new Product {ProductID = 3, Name = "third", CategoryID = 3},
            }.AsQueryable);

            var controller = new ProductController(mock.Object);
            var result = controller.List(1);
            var model = (IEnumerable<Product>) (result).Model;
            var array = model.ToArray();
            Assert.Equal(2, model.Count());
            Assert.Equal("first", array[0].Name);
            Assert.Equal("first2",array[1].Name);
        }

        [Fact]
        public void ReturningProductByCategory_ReturnsEmptyCollection()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "first", CategoryID = 1},
                new Product {ProductID = 1, Name = "first2", CategoryID = 1},
                new Product {ProductID = 2, Name = "second", CategoryID = 2},
                new Product {ProductID = 3, Name = "third", CategoryID = 3},
            }.AsQueryable);

            var controller = new ProductController(mock.Object);
            var result = controller.List(4);
            var model = (IEnumerable<Product>) result.Model;
            Assert.Empty(model);
        }

        [Fact]
        public void ReturningProductById_ShouldReturnEmptyCollection()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "first", CategoryID = 1},
                new Product {ProductID = 2, Name = "second", CategoryID = 2},
                new Product {ProductID = 3, Name = "third", CategoryID = 3},
            }.AsQueryable);

            var controller = new ProductController(mock.Object);
            var result = controller.ProductId(4);
            var model = (IEnumerable<Product>) result.Model;
            Assert.Empty(model);
        }
    }
}
