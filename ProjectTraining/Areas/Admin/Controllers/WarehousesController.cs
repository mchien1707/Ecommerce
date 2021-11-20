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
    public class WarehousesController : Controller
    {
        private readonly ProjectTrainingContext _context;

        public WarehousesController(ProjectTrainingContext context)
        {
            _context = context;
        }
        public int numberPage(int Total, int limit)
        {
            float numberpage = (float)Total / (float)limit;
            return (int)Math.Ceiling(numberpage);
        }
        // GET: Admin/Warehouses
        public async Task<IActionResult> Index(int? page=0)
        {
            var projectTrainingContext = await _context.Warehouse.Include(w => w.Product).ThenInclude(x => x.Kind).ToListAsync();

            int limit = 8;
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
            var data = projectTrainingContext.OrderByDescending(x => x.ProductId).Skip(start).Take(limit);

            return View(data);
        }
        public IActionResult Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = _context.Warehouse
                .Include(w => w.Product).ThenInclude(w => w.Kind)
                .Include(w => w.Product).ThenInclude(w => w.Discount)
                .FirstOrDefault(x => x.WarehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return View(warehouse);
        }

        // POST: Admin/Warehouses/Create
        [HttpPost]
        public async Task<IActionResult> Add(int id, ProductEditWarehouse prod)
        {
            if (id != prod.WarehouseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Product updateProduct = new Product()
                    {
                        ProductId = prod.ProductId,
                        ProductDescription = prod.ProductDescription,
                        ProductImage = prod.ProductImage,
                        ProductName = prod.ProductName,
                        ProductPrice = prod.ExportPrice,
                        DiscountId = prod.DiscountId,
                        KindId = prod.KindId,
                        CreateDate = prod.CreateDate,
                        Quantities = prod.Quantities + prod.SLnhap,
                    };
                    Warehouse warehouse = new Warehouse()
                    {
                        WarehouseId = prod.WarehouseId,
                        ProductId = prod.ProductId,
                        Quantities = prod.Quantities + prod.SLnhap,
                        ImportPrice = prod.ImportPrice,
                        ExportPrice = prod.ExportPrice,
                        DateImport = DateTime.Now,
                        Solded = prod.Solded,
                    };
                    _context.Product.Update(updateProduct);
                    _context.Warehouse.Update(warehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists(prod.WarehouseId))
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
            return View(prod);
        }

        // GET: Admin/Warehouses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = _context.Warehouse
                .Include(w => w.Product).ThenInclude(w => w.Kind)
                .Include(w=> w.Product).ThenInclude(w=> w.Discount)
                .FirstOrDefault(x => x.WarehouseId == id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return View(warehouse);
        }

        // POST: Admin/Warehouses/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductEditWarehouse prod)
        {
            if (id != prod.WarehouseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Product updateProduct = new Product()
                    {
                        ProductId = prod.ProductId,
                        ProductDescription = prod.ProductDescription,
                        ProductImage = prod.ProductImage,
                        ProductName = prod.ProductName,
                        ProductPrice = prod.ExportPrice,
                        DiscountId = prod.DiscountId,
                        KindId = prod.KindId,
                        CreateDate = prod.CreateDate,
                        Quantities = prod.Quantities,
                    };
                    Warehouse warehouse = new Warehouse()
                    {
                        WarehouseId = prod.WarehouseId,
                        ProductId = prod.ProductId,
                        Quantities = prod.Quantities,
                        ImportPrice = prod.ImportPrice,
                        ExportPrice = prod.ExportPrice,
                        DateImport = prod.DateImport,
                        Solded = prod.Solded,
                    };
                    _context.Product.Update(updateProduct);
                    _context.Warehouse.Update(warehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseExists(prod.WarehouseId))
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
            return View(prod);
        }

        //// GET: Admin/Warehouses/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var warehouse = await _context.Warehouse
        //        .Include(w => w.Product)
        //        .FirstOrDefaultAsync(m => m.WarehouseId == id);
        //    if (warehouse == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(warehouse);
        //}

        //// POST: Admin/Warehouses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var warehouse = await _context.Warehouse.FindAsync(id);
        //    _context.Warehouse.Remove(warehouse);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool WarehouseExists(int id)
        {
            return _context.Warehouse.Any(e => e.WarehouseId == id);
        }
    }
}
