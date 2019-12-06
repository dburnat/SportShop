using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportShop.Models;

namespace SportShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductApiController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Returns list of all products currently stored in database
        /// </summary>
        /// <returns>List of all products</returns>
        [HttpGet]
        public IEnumerable<Product> Get() => _productRepository.Products;
        /// <summary>
        /// Returns product with specific id
        /// </summary>
        /// <param name="id">Integer</param>
        /// <returns>Product</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Product product = _productRepository.Products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="product">Product object</param>
        /// <returns>Product that was created</returns>
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _productRepository.SaveProduct(product);

            return CreatedAtAction(nameof(GetById), new { id = product.ProductID }, product);
        }
        /// <summary>
        /// Edits a product with specific id
        /// </summary>
        /// <param name="id">Integer</param>
        /// <returns>Product that was edited</returns>
        [HttpPut("{id}")]
        public IActionResult Edit(int id)
        {
            Product product = _productRepository.Products.FirstOrDefault(p => p.ProductID == id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (product == null)
            {
                return NotFound();
            }

            try
            {
                _productRepository.SaveProduct(product);
            }
            catch (DbUpdateException)
            {
                if (_productRepository.Products.FirstOrDefault(p => p.ProductID == id) !=null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetById), new { id = product.ProductID }, product);
        }
        /// <summary>
        /// Deletes product with specific id
        /// </summary>
        /// <param name="id">Integer</param>
        /// <returns>StatusCode200</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _productRepository.Products.FirstOrDefault(p => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            _productRepository.DeleteProduct(id);

            return Ok(product);
        }
       
    }
}