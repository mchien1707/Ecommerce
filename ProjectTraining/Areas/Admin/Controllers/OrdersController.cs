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
    public class OrdersController : Controller
    {
        private readonly ProjectTrainingContext _context;

        public OrdersController(ProjectTrainingContext context)
        {
            _context = context;
        }
        public int numberPage(int Total, int limit)
        {
            float numberpage = (float)Total / (float)limit;
            return (int)Math.Ceiling(numberpage);
        }
        // GET: Admin/Orders
        public async Task<IActionResult> Index(int? page=0)
        {
            var projectTrainingContext = await _context.Order.Include(o => o.User).ToListAsync();
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
            var data = projectTrainingContext.OrderByDescending(x => x.OrderId).Skip(start).Take(limit);
            return View(data);
        }

        // GET: Admin/Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = _context.OrderDetails.Include(o => o.Order).Include(o => o.Product).Where(x => x.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        public async Task<IActionResult> CancelOrder(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var order = await _context.Order.FirstOrDefaultAsync(x => x.OrderId == id);
            List<OrderDetails> orderDetails = _context.OrderDetails.Where(x => x.OrderId == id).ToList();
            foreach (var item in orderDetails)
            {
                var product = _context.Product.FirstOrDefault(x => x.ProductId == item.ProductId);
                product.Quantities += item.Quantities;
                var warehouse = _context.Warehouse.FirstOrDefault(x => x.ProductId == item.ProductId);
                warehouse.Quantities += item.Quantities;
                warehouse.Solded -= item.Quantities;

                _context.OrderDetails.Remove(item);
            }
            _context.Order.Remove(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult UpdateOrder(int? status, int? typePayment, int? paymentResult, int? OrderId)
        {
            if(OrderId == null)
            {
                return NotFound();
            }
            var order = _context.Order.FirstOrDefault(x => x.OrderId == OrderId);
            if (status != null)
            {
                if(status < 2)
                {
                    order.Status = status + 1;
                }
            }
            if (typePayment != null)
            {
                if(typePayment == 0)
                {
                    order.Type = typePayment + 1;
                }
                else
                {
                    order.Type = typePayment - 1;
                }
            }
            if (paymentResult != null)
            {
                if(paymentResult == 0)
                {
                    order.PaymentResult = paymentResult + 1;
                }
                else
                {
                    order.PaymentResult = paymentResult - 1;
                }
            }
            _context.Order.Update(order);
            _context.SaveChanges();
            return Ok();
        }
    }
}
