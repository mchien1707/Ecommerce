using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.Controllers
{
    public class CartController : Controller
    {
        private readonly ProjectTrainingContext _context;
        public CartController(ProjectTrainingContext context)
        {
            _context = context;
        }
        public Product getDetailProduct(int id)
        {
            var product = _context.Product.Include(o => o.Discount).FirstOrDefault(m => m.ProductId == id);
            return product;
        }
        [HttpGet]
        public IActionResult AddToCart(int id, int quantities)
        {
            var product = getDetailProduct(id);
            var cart = HttpContext.Session.GetString("cart");
            if (cart == null)
            {
                List<ItemCart> newItem = new List<ItemCart>()
                {
                    new ItemCart()
                    {
                        ProductId = id,
                        Quantities = quantities,
                        Product = product,
                    },
                };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(newItem, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                }));
            }
            else
            {
                List<ItemCart> dataCart = JsonConvert.DeserializeObject<List<ItemCart>>(cart);
                bool Check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if(dataCart[i].ProductId == id)
                    {
                        dataCart[i].Quantities += quantities;
                        Check = false;
                    }
                }
                if(Check == true)
                {
                    dataCart.Add(new ItemCart()
                    {
                        ProductId = id,
                        Quantities = quantities,
                        Product = product,
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                }));
            }
            return Ok(quantities);
        }
        [Route("/Cart")]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetString("cart");
            if(cart != null)
            {
                List<ItemCart> dataCart = JsonConvert.DeserializeObject<List<ItemCart>>(cart);
                return View(dataCart);
            }
            return View();
        }
        [HttpPost]
        public IActionResult UpdateCart(int id, int quantities)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<ItemCart> dataCart = JsonConvert.DeserializeObject<List<ItemCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].ProductId == id)
                    {
                        dataCart[i].Quantities = quantities;
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                }));
            }
            return Ok(quantities);
        }
        public IActionResult DeleteItemCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<ItemCart> dataCart = JsonConvert.DeserializeObject<List<ItemCart>>(cart);
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].ProductId == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                }));
                return Ok(dataCart.Count);
            }
            return Ok();
        }
    }
}
