using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportShop.Models;

namespace SportShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List(int ID) => View(repository.Products.Where(c => c.CategoryID == ID));

        public ViewResult ProductId(int ID) => View(repository.Products.Where(b => b.ProductID == ID));
    }
}