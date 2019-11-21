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
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AdminController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult Index() => View(_productRepository.Products);

        public ViewResult Edit(int id)
        {
            Product product = _productRepository.Products.First(c => c.ProductID == id);
            PopulateCategoriesDropDownList(product.CategoryID);
            return View(_productRepository.Products.FirstOrDefault(p => p.ProductID == id));
        }

        [HttpPost]
        public IActionResult Save(Product product)
        {
            if (ModelState.IsValid)
            {
                _productRepository.SaveProduct(product);
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
