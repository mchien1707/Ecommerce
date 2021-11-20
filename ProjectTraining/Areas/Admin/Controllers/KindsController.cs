using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTraining.Models;

namespace ProjectTraining.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KindsController : Controller
    {
        private readonly ProjectTrainingContext _context;

        public KindsController(ProjectTrainingContext context)
        {
            _context = context;
        }
        public int numberPage(int Total, int limit)
        {
            float numberpage = (float)Total / (float)limit;
            return (int)Math.Ceiling(numberpage);
        }
        // GET: Admin/Kinds
        public async Task<IActionResult> Index(int? page=0)
        {
            var projectTrainingContext = await _context.Kind.ToListAsync();
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
            var data = projectTrainingContext.OrderByDescending(x => x.KindId).Skip(start).Take(limit);
            return View(data);
        }

        // GET: Admin/Kinds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Kinds/Create
        [HttpPost]
        public async Task<IActionResult> Create(Kind kind)
        {
            if (ModelState.IsValid)
            {
                Kind newKind = new Kind()
                {
                    KindName = kind.KindName,
                    CreateDate = DateTime.Now,
                };
                _context.Add(newKind);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kind);
        }

        // GET: Admin/Kinds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kind = await _context.Kind.FindAsync(id);
            if (kind == null)
            {
                return NotFound();
            }
            return View(kind);
        }

        // POST: Admin/Kinds/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Kind kind)
        {
            if (id != kind.KindId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Kind kindUpdate = new Kind()
                    {
                        KindId = kind.KindId,
                        KindName = kind.KindName,
                        CreateDate = (DateTime)kind.CreateDate,
                    };
                    _context.Update(kindUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KindExists(kind.KindId))
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
            return View(kind);
        }
        private bool KindExists(int id)
        {
            return _context.Kind.Any(e => e.KindId == id);
        }
    }
}
