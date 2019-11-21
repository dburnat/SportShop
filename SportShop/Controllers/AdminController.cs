using Microsoft.AspNetCore.Mvc;
using SportShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SportShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public AdminController(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            this._repository = repository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult Index() => View(_repository.Products);

        public ViewResult Edit(int id) => View(_repository.Products.FirstOrDefault(p => p.ProductID == id));

        [HttpPost]
        public IActionResult Save(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
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
            Product productToDelete = _repository.DeleteProduct(product.ProductID);

            if (productToDelete != null)
            {
            TempData["message"] = $"Usunięto {productToDelete.Name}.";
            }
            return RedirectToAction("Index");
        }
    }
}
