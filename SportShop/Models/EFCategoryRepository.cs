using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportShop.Models
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EFCategoryRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Category> Categories => dbContext.Categories;
    }
}
