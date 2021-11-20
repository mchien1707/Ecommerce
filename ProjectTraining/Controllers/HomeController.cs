using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectTraining.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjectTrainingContext _context;

        public HomeController(ProjectTrainingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Product.Include(o => o.Discount).Include(o => o.Kind).OrderByDescending(o => o.CreateDate);
            return View(products);
        }
        public int numberPage(int Total, int limit)
        {
            float numberpage = (float)Total / (float)limit;
            return (int)Math.Ceiling(numberpage);
        }
        public IActionResult Shopping(int? page=0)
        {
            var products = _context.Product.Include(o => o.Discount).Include(o => o.Kind).ToList();
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
            int totalProduct = products.Count();
            ViewBag.totalProduct = totalProduct;
            ViewBag.numberPage = numberPage(totalProduct, limit);
            var data = products.OrderByDescending(x => x.ProductId).Skip(start).Take(limit);
            return View(data);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _context.Product
                .Include(o => o.Discount)
                .Include(o => o.Comments).ThenInclude(x => x.User)
                .Include(o => o.Kind)
                .FirstOrDefault(m => m.ProductId == id);
            product.Comments = product.Comments.OrderByDescending(c => c.CommentId).ToList(); 
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        public IActionResult ProductsBrand(int? id, int? page=0)
        {
            var products = _context.Product.Include(o => o.Discount).Include(o => o.Kind).Where(w => w.KindId == id).ToList();
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
            int totalProduct = products.Count();
            ViewBag.totalProduct = totalProduct;
            ViewBag.numberPage = numberPage(totalProduct, limit);
            var data = products.OrderByDescending(x => x.ProductId).Skip(start).Take(limit);
            return View(data);
        }
        [HttpPost]
        public IActionResult AddComment(int ProductId, int UserId, int Rate, string Comment)
        {
            Comments newComment = new Comments() {
                ProductId = ProductId,
                UserId = UserId,
                Rate = Rate,
                Comment = Comment,
                CreateDate = DateTime.Now,
            };
            _context.Add(newComment);
            _context.SaveChanges();
            return Ok(newComment);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
