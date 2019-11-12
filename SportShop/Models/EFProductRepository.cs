using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportShop.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EFProductRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IQueryable<Product> Products =>  dbContext.Products;

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                dbContext.Products.Add(product);
            }
            else
            {
                Product productToEdit = dbContext.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (productToEdit != null)
                {
                    productToEdit.Name = product.Name;
                    productToEdit.Price = product.Price;
                    productToEdit.Description = product.Description;
                    productToEdit.Rating = product.Rating;
                }
            }
            dbContext.SaveChanges();
        }

        public Product DeleteProduct(int id)
        {
            Product productToDelete = dbContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (productToDelete != null)
            {
                dbContext.Products.Remove(productToDelete);
                dbContext.SaveChanges();
            }
            return productToDelete;
        }
    }
}
