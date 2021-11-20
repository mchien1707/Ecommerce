using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTraining.Models;

namespace ProjectTraining.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiscountsController : Controller
    {
        private readonly ProjectTrainingContext _context;

        public DiscountsController(ProjectTrainingContext context)
        {
            _context = context;
        }
        public int numberPage(int Total, int limit)
        {
            float numberpage = (float)Total / (float)limit;
            return (int)Math.Ceiling(numberpage);
        }
        // GET: Admin/Discounts
        public async Task<IActionResult> Index(int? page=0)
        {
            var projectTrainingContext = await _context.Discount.ToListAsync();
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
            var data = projectTrainingContext.OrderByDescending(x => x.DiscountId).Skip(start).Take(limit);
            return View(data);
        }

        // GET: Admin/Discounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discount = await _context.Discount
                .FirstOrDefaultAsync(m => m.DiscountId == id);
            if (discount == null)
            {
                return NotFound();
            }

            return View(discount);
        }

        // GET: Admin/Discounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Discounts/Create
        [HttpPost]
        public async Task<IActionResult> Create(Discount discount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discount);
        }
        public string GetDate(string date)
        {
            string result = "";
            for(int i=0; i<10; i++)
            {
                result += date[i];
            }
            return result;
        }

        // GET: Admin/Discounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var discount = await _context.Discount.FindAsync(id);
            if (discount == null)
            {
                return NotFound();
            }
            return View(discount);
        }

        // POST: Admin/Discounts/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Discount discount)
        {
            if (id != discount.DiscountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Discount updateDiscount = new Discount()
                    {
                        DiscountId = discount.DiscountId,
                        DiscountPercent = discount.DiscountPercent,
                        DiscountStart = discount.DiscountStart,
                        DiscountEnd = discount.DiscountEnd,
                        DiscountDescription = discount.DiscountDescription
                    };
                    _context.Update(updateDiscount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscountExists(discount.DiscountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discount);
        }
        public async Task<IActionResult> Apply(int? id)
        {
            var discount = await _context.Discount.FindAsync(id);
            SelectList kind = new SelectList(_context.Kind, "KindId", "KindName");
            ViewBag.kindList = kind;
            return View(discount);
        }
        
        [HttpPost]
        public async Task<IActionResult> Apply(int DiscountId, int KindId)
        {
            var productApplyDiscount = await _context.Product
                                        .Include(o => o.Kind)
                                        .Include(o => o.Discount)
                                        .Where(x => x.KindId == KindId).ToListAsync();
            foreach(var item in productApplyDiscount)
            {
                item.DiscountId = DiscountId;
                _context.Product.Update(item);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool DiscountExists(int id)
        {
            return _context.Discount.Any(e => e.DiscountId == id);
        }
    }
}
