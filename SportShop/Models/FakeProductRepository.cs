using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportShop.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product { Name = "piłka", Price = 20},
            new Product { Name = "ochraniacze", Price = 22},
            new Product { Name = "korki", Price = 23},
        }.AsQueryable<Product>();
    }
}
