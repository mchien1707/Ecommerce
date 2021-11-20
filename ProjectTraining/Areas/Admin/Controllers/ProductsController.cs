using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTraining.Models;

namespace ProjectTraining.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ProjectTrainingContext _context;

        public ProductsController(ProjectTrainingContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public int numberPage(int Total, int limit)
        {
            float numberpage = (float)Total / (float)limit;
            return (int)Math.Ceiling(numberpage);
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(int? page = 0)
        {
            var projectTrainingContext = await _context.Product.Include(p => p.Discount).Include(p => p.Kind).ToListAsync();
            int limit = 8;
            int start;
            if(page > 0)
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

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Discount)
                .Include(p => p.Kind)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            SelectList discount = new SelectList(_context.Discount, "DiscountId", "DiscountPercent");
            SelectList kind = new SelectList(_context.Kind, "KindId", "KindName");
            ViewBag.discountList = discount;
            ViewBag.kindList = kind;
            return View();
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
        // POST: Admin/Products/Create
        [HttpPost]
        //Đoạn này đang lối
        public async Task<IActionResult> Create(ProductData product)
        {
            string uniqueFileName = GetUniqueFileName(product.ProductImage.FileName);
            if (ModelState.IsValid)
            {
                //Add product to wwwroot
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "imgProduct");
                var filePath = Path.Combine(uploads, uniqueFileName);
                product.ProductImage.CopyTo(new FileStream(filePath, FileMode.Create));

                //Add product to DB
                Product newProduct = new Product()
                {
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductImage = uniqueFileName,
                    KindId = product.KindId,
                    DiscountId = product.DiscountId,
                    ProductDescription = product.ProductDescription,
                    CreateDate = DateTime.Now,
                };
                await _context.Product.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                var prd = _context.Product
                    .Where(x => x.ProductName == product.ProductName)
                    .FirstOrDefault(o => o.ProductName == product.ProductName);
                Warehouse ProductTowarehouse = new Warehouse()
                {
                    ProductId = prd.ProductId,
                    Quantities = product.Quantities,
                    ImportPrice = product.ImportPrice,
                    ExportPrice = product.ProductPrice,
                    DateImport = DateTime.Now,
                    Solded = 0,
                };
                _context.Warehouse.Add(ProductTowarehouse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            ProductData productData = new ProductData()
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductImageName = product.ProductImage,
                ProductDescription = product.ProductDescription,
                CreateDate = product.CreateDate,
                KindId = product.KindId,
                DiscountId = product.DiscountId,
                Quantities = product.Quantities,
            };
            if (product == null)
            {
                return NotFound();
            }
            SelectList discount = new SelectList(_context.Discount, "DiscountId", "DiscountPercent", product.DiscountId);
            SelectList kind = new SelectList(_context.Kind, "KindId", "KindName", product.KindId);
            ViewBag.discountList = discount;
            ViewBag.kindList = kind;
            return View(productData);
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductData product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Product productUpdate;
                    if(product.ProductImage == null)
                    {
                        productUpdate = new Product()
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            ProductPrice = product.ProductPrice,
                            ProductImage = product.ProductImageName,
                            ProductDescription = product.ProductDescription,
                            CreateDate = product.CreateDate,
                            KindId = product.KindId,
                            DiscountId = product.DiscountId,
                            Quantities = product.Quantities,
                        };
                    }
                    else
                    {
                        string uniqueFileName = GetUniqueFileName(product.ProductImage.FileName);
                        //Add product to wwwroot
                        var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "imgProduct");
                        var filePath = Path.Combine(uploads, uniqueFileName);
                        product.ProductImage.CopyTo(new FileStream(filePath, FileMode.Create));

                        //Add product to DB
                        productUpdate = new Product()
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            ProductPrice = product.ProductPrice,
                            ProductImage = uniqueFileName,
                            ProductDescription = product.ProductDescription,
                            CreateDate = product.CreateDate,
                            KindId = product.KindId,
                            DiscountId = product.DiscountId,
                            Quantities = product.Quantities,
                        };
                    }

                    _context.Update(productUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["DiscountId"] = new SelectList(_context.Discount, "DiscountId", "DiscountId", product.DiscountId);
            ViewData["KindId"] = new SelectList(_context.Kind, "KindId", "KindId", product.KindId);
            return View(product);
        }

        //// GET: Admin/Products/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Product
        //        .Include(p => p.Discount)
        //        .Include(p => p.Kind)
        //        .FirstOrDefaultAsync(m => m.ProductId == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        //// POST: Admin/Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var product = await _context.Product.FindAsync(id);
        //    _context.Product.Remove(product);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
