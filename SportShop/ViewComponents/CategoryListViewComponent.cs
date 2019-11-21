using SportShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SportShop.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryListViewComponent( ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await GetCategoriesAsync();
            return View(categories);
        }
        private Task<List<Category>> GetCategoriesAsync()
        {
            return dbContext.Categories.Distinct().ToListAsync();
        }

    }
}
