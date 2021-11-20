using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.Controllers
{
    public class SearchController : Controller
    {
        private readonly ProjectTrainingContext _context;
        public SearchController(ProjectTrainingContext context)
        {
            _context = context;
        }
        public int numberPage(int Total, int limit)
        {
            float numberpage = (float)Total / (float)limit;
            return (int)Math.Ceiling(numberpage);
        }
        [Route("/Search/{key?}")]
        public IActionResult Index(string key, int? page=0)
        {
            if(key == null)
            {
                return NotFound();
            }
            var result = _context.Product
                .Include(x => x.Kind)
                .Include(x => x.Discount)
                .Where(x => x.ProductName.Contains(key))
                .ToList();

            int limit = 12;
            int start;
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            start = (int)(page - 1) * limit;
            ViewBag.pageCurrent = page;
            int totalProduct = result.Count();
            ViewBag.totalProduct = totalProduct;
            ViewBag.numberPage = numberPage(totalProduct, limit);
            var data = result.OrderByDescending(x => x.ProductId).Skip(start).Take(limit);

            ViewBag.SearchString = key;
            return View(data);
        }
    }
}