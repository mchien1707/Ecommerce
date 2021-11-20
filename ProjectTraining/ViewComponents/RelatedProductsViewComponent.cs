using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.ViewComponents
{
    public class RelatedProductsViewComponent : ViewComponent
    {
        private readonly ProjectTrainingContext _context;
        public RelatedProductsViewComponent(ProjectTrainingContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listProductRelated = await _context.Warehouse
            .Include(o => o.Product).ThenInclude(u => u.Discount).OrderByDescending(x => x.Solded).ToListAsync();

            List<Warehouse> products = new List<Warehouse>();

            for (int i = 0; i < 4; i++)
            {
                products.Add(listProductRelated[i]);
             }
            return View(products);
        }
    }
}
