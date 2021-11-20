using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.Controllers
{
    public class FilterController : Controller
    {
        private readonly ProjectTrainingContext _context;
        public FilterController(ProjectTrainingContext context)
        {
            _context = context;
        }
        public int numberPage(int Total, int limit)
        {
            float numberpage = (float)Total / (float)limit;
            return (int)Math.Ceiling(numberpage);
        }
        [Route("/Filter/{options?}")]
        public IActionResult Index(string option, int? page=0)
        {
            if (option == "none")
            {
                var product = _context.Product.Include(x => x.Kind).Include(x => x.Discount).ToList();
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
                int totalProduct = product.Count();
                ViewBag.totalProduct = totalProduct;
                ViewBag.numberPage = numberPage(totalProduct, limit);
                var data = product.Skip(start).Take(limit);

                ViewBag.option = option;
                return View(data);
            }
            if (option == "nameASC")
            {
                var product = _context.Product.Include(x => x.Kind).Include(x => x.Discount).OrderBy(o => o.ProductName).ToList();
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
                int totalProduct = product.Count();
                ViewBag.totalProduct = totalProduct;
                ViewBag.numberPage = numberPage(totalProduct, limit);
                var data = product.Skip(start).Take(limit);

                ViewBag.option = option;
                return View(data);
            }
            if (option == "nameDESC")
            {
                var product = _context.Product.Include(x => x.Kind).Include(x => x.Discount).OrderByDescending(o => o.ProductName).ToList();
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
                int totalProduct = product.Count();
                ViewBag.totalProduct = totalProduct;
                ViewBag.numberPage = numberPage(totalProduct, limit);
                var data = product.Skip(start).Take(limit);

                ViewBag.option = option;
                return View(data);
            }
            if (option == "PriceDESC")
            {
                var product = _context.Product.Include(x => x.Kind).Include(x => x.Discount).OrderByDescending(o => o.ProductPrice).ToList();
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
                int totalProduct = product.Count();
                ViewBag.totalProduct = totalProduct;
                ViewBag.numberPage = numberPage(totalProduct, limit);
                var data = product.Skip(start).Take(limit);

                ViewBag.option = option;
                return View(data);
            }
            if (option == "PriceASC")
            {
                var product = _context.Product.Include(x => x.Kind).Include(x => x.Discount).OrderBy(o => o.ProductPrice).ToList();
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
                int totalProduct = product.Count();
                ViewBag.totalProduct = totalProduct;
                ViewBag.numberPage = numberPage(totalProduct, limit);
                var data = product.Skip(start).Take(limit);

                ViewBag.option = option;
                return View(data);
            }
            if (option == "DateDESC")
            {
                var product = _context.Product.Include(x => x.Kind).Include(x => x.Discount).OrderByDescending(o => o.CreateDate).ToList();
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
                int totalProduct = product.Count();
                ViewBag.totalProduct = totalProduct;
                ViewBag.numberPage = numberPage(totalProduct, limit);
                var data = product.Skip(start).Take(limit);

                ViewBag.option = option;
                return View(data);
            }
            if (option == "DateASC")
            {
                var product = _context.Product.Include(x => x.Kind).Include(x => x.Discount).OrderBy(o => o.CreateDate).ToList();
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
                int totalProduct = product.Count();
                ViewBag.totalProduct = totalProduct;
                ViewBag.numberPage = numberPage(totalProduct, limit);
                var data = product.Skip(start).Take(limit);

                ViewBag.option = option;
                return View(data);
            }
            return View();
        }
    }
}
