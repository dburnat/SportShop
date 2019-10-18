using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportShop.Models
{
    public class EFManufacturerRepository : IManufacturerRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EFManufacturerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IQueryable<Manufacturer> Manufacturers => dbContext.Manufacturers.Include( c=> c.Products);
    }
}
