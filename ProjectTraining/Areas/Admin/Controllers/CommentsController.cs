using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentsController : Controller
    {
        private readonly ProjectTrainingContext _context;
        public CommentsController(ProjectTrainingContext context)
        {
            _context = context;
        }
        public int numberPage(int Total, int limit)
        {
            float numberpage = (float)Total / (float)limit;
            return (int)Math.Ceiling(numberpage);
        }
        public async Task<IActionResult> Index(int? page=0)
        {
            var projectTrainingContext = await _context.Comments.Include(x => x.Product).Include(x => x.User).ToListAsync();
            int limit = 4;
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
            int totalProduct = projectTrainingContext.Count();
            ViewBag.totalProduct = totalProduct;
            ViewBag.numberPage = numberPage(totalProduct, limit);
            var data = projectTrainingContext.OrderByDescending(x => x.CommentId).Skip(start).Take(limit);
            return View(data);
        }
        [HttpPost]
        public  IActionResult Delete(int id)
        {
            var comment = _context.Comments.Find(id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
