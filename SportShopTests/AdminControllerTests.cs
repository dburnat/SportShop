using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportShop.Controllers;
using SportShop.Models;
using Xunit;

namespace SportShopTests
{
    public class AdminControllerTests
    {
        [Fact]
        public void IndexReturnsAllProducts_ShouldReturnCollectionContainingEveryProduct()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "first"},
                new Product {ProductID = 2, Name = "second"},
                new Product {ProductID = 3, Name = "third"},
                new Product {ProductID = 4, Name = "fourth", CategoryID = 15},
            }.AsQueryable);


            var controller = new AdminController(mock.Object);
            
            var result = controller.Index();

            var model = (IEnumerable<Product>)(result).Model;
            var array = model.ToArray();

            Assert.Equal(4, model.Count());
            Assert.Equal("first",array[0].Name);
            Assert.Equal("second",array[1].Name);
            Assert.Equal("third",array[2].Name);
            Assert.Equal("fourth", array[3].Name);
        }
        [Fact]
        public void DeletingProduct_ShouldBeCalledOnlyOnce()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "first", CategoryID = 1},
                new Product {ProductID = 2, Name = "second", CategoryID = 2},
                new Product {ProductID = 3, Name = "third", CategoryID = 3},
            }.AsQueryable);

            var controller = new AdminController(mock.Object);
            controller.Delete(mock.Object.Products.ElementAt(0));
            mock.Verify(m => m.DeleteProduct(mock.Object.Products.ElementAt(0).ProductID), Times.Once());
        }

        [Fact]
        public void SavingProductWithModelError_ShouldReturnEditViewResult()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "first", CategoryID = 1},
                new Product {ProductID = 2, Name = "second", CategoryID = 2},
                new Product {ProductID = 3, Name = "third", CategoryID = 3},
            }.AsQueryable);
            var product = new Product
            {
                ProductID = 5, Name = "fifth"
            };

            var controller = new AdminController(mock.Object);
            controller.ModelState.AddModelError("error", "errormessage");

            var result = controller.Save(product) as ViewResult;
            Assert.Equal("Edit", result.ViewName);
        }
    }
}
