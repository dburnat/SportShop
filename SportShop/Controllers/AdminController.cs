using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository repository;

        public AdminController(IProductRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult Index() => View(repository.Products);

        public ViewResult Edit(int id) => View(repository.Products.FirstOrDefault(p => p.ProductID == id));

        [HttpPost]
        public IActionResult Save(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"Zapisano {product.Name}.";
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", product);
            }
        }

        public ViewResult Create() =>  View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            Product productToDelete = repository.DeleteProduct(product.ProductID);

            if (productToDelete != null)
            {
            TempData["message"] = $"Usunięto {product.Name}.";
            }
            return RedirectToAction("Index");
        }
    }
}
