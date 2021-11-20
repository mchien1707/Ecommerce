using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using ProjectTraining.Models;
using ProjectTraining.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly string vnp_Url = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private readonly string vnp_Returnurl = "https://localhost:44354/Checkout/ResultCheckout";
        private readonly string vnp_TmnCode = "UDOPNWS1";
        private readonly string vnp_HashSecret = "EBAHADUGCOEWYXCMYZRMTMLSHGKNRPBN";
        public readonly ProjectTrainingContext _context;
        public CheckoutController(ProjectTrainingContext context)
        {
            _context = context;
        }
        [Route("/Checkout")]
        public async Task<IActionResult> Index()
        {
            var user = HttpContext.Session.GetString("userLogin");
            var cart = HttpContext.Session.GetString("cart"); 
            if (user == null || cart == null)
            {
                return Redirect("~/Home");
            }
            if (user != null && cart != null)
            {
                List<ItemCart> dataCart = JsonConvert.DeserializeObject<List<ItemCart>>(cart);
                UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(user);
                var getUser = _context.User.FirstOrDefault(x => x.UserId == userInfo.UserId);
                float TotalPayment = 0;
                foreach (var item in dataCart)
                {
                    if (item.Product.DiscountId != null)
                    {
                        if (item.Product.Discount.DiscountStart < DateTime.Now && DateTime.Now < item.Product.Discount.DiscountEnd)
                        {
                            TotalPayment += ((float)(item.Product.ProductPrice - (item.Product.ProductPrice * item.Product.Discount.DiscountPercent) / 100)) * (int)item.Quantities;
                        }
                        else
                        {
                            TotalPayment += (float)item.Product.ProductPrice * (int)item.Quantities;
                        }
                    }
                    else
                    {
                        TotalPayment += (float)item.Product.ProductPrice * (int)item.Quantities;
                    }
                }
                DateTime createDate = DateTime.Now;
                Order newOrder = new Order()
                {
                    Total = TotalPayment,
                    Status = 0,
                    CreateDate = createDate,
                    UserId = userInfo.UserId,
                    Type = 0,
                    PaymentResult = 0,
                    Note = null,
                };
                _context.Order.Add(newOrder);
                await _context.SaveChangesAsync();
                var order = _context.Order
                    .Where(x => x.CreateDate == createDate)
                    .Where(x => x.UserId == userInfo.UserId)
                    .FirstOrDefault(x => x.UserId == userInfo.UserId);
                foreach(var item in dataCart)
                {
                    float totalPriceProduct = 0;
                    if (item.Product.DiscountId != null)
                    {
                        if (item.Product.Discount.DiscountStart < DateTime.Now && DateTime.Now < item.Product.Discount.DiscountEnd)
                        {
                            totalPriceProduct = ((float)(item.Product.ProductPrice - (item.Product.ProductPrice * item.Product.Discount.DiscountPercent) / 100)) * (int)item.Quantities;
                        }
                        else
                        {
                            totalPriceProduct = (float)item.Product.ProductPrice * (int)item.Quantities;
                        }
                    }
                    else
                    {
                        totalPriceProduct = (float)item.Product.ProductPrice * (int)item.Quantities;
                    }
                    OrderDetails orderDetails = new OrderDetails()
                    {
                        OrderId = order.OrderId,    
                        ProductId = item.ProductId,
                        Quantities = item.Quantities,
                        TotalPrice = totalPriceProduct,
                    };
                    var product = _context.Product.FirstOrDefault(x => x.ProductId == item.ProductId);
                    product.Quantities -= item.Quantities;
                    var warehouse = _context.Warehouse.FirstOrDefault(x => x.ProductId == item.ProductId);
                    warehouse.Quantities -= item.Quantities;
                    warehouse.Solded += item.Quantities;

                    _context.Product.Update(product);
                    _context.Warehouse.Update(warehouse);

                    _context.OrderDetails.Add(orderDetails);
                    
                }
                await _context.SaveChangesAsync();
                ViewBag.OrderId = order.OrderId;
                CartCheckout cartCheckout = new CartCheckout()
                {
                    ListItemCart = dataCart,
                    User = getUser,
                };
                return View(cartCheckout);
            }
            return View();
        }
        [HttpPost]
        public IActionResult ConfirmCheckout(int OrderId, string VnpayType, string onDelivery, string note)
        {
            var user = HttpContext.Session.GetString("userLogin");
            var cart = HttpContext.Session.GetString("cart");
            if (user == null || cart == null || VnpayType == null)
            {
                return Redirect("~/Home");
            }
            var order = _context.Order.FirstOrDefault(x => x.OrderId == OrderId);
            if (onDelivery == null)
            {
                //Build URL for VNPAY
                VnPayLibrary vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", "2.0.0");
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                vnpay.AddRequestData("vnp_Locale", "vn");

                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_TxnRef", OrderId.ToString());
                vnpay.AddRequestData("vnp_OrderInfo", note);
                vnpay.AddRequestData("vnp_Amount", (order.Total * 100).ToString());
                vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
                vnpay.AddRequestData("vnp_IpAddr", "192.168.43.23");
                vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_BankCode", VnpayType);
                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

                order.Type = 1;
                order.Note = note;
                _context.Order.Update(order);
                _context.SaveChanges();
                return Redirect(paymentUrl);
            }
            else
            {
                order.Note = note;
                HttpContext.Session.Remove("cart");
                return Redirect("~/Home");
            }
        }
        public IActionResult ResultCheckout()
        {
            var user = HttpContext.Session.GetString("userLogin");
            var cart = HttpContext.Session.GetString("cart");
            if (user == null || cart == null)
            {
                return Redirect("~/Home");
            }
            var vnpayData = HttpContext.Request.Query.ToList();
            VnPayLibrary vnpay = new VnPayLibrary();

            foreach (var s in vnpayData)
            {
                var value = s.Value;
                //get all querystring data
                if (!string.IsNullOrEmpty(s.Value))
                {
                    vnpay.AddResponseData(s.Key, s.Value);
                }
            }
            //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
            int orderId = Convert.ToInt32(vnpay.GetResponseData("vnp_TxnRef"));

            //vnp_TransactionNo: Ma GD tai he thong VNPAY

            long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            //vnp_SecureHash: MD5 cua du lieu tra ve
            string vnp_SecureHash = Request.Query["vnp_SecureHash"];
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
            if (checkSignature)
            {
                if (vnp_ResponseCode == "00")
                {
                    var order = _context.Order.FirstOrDefault(x => x.OrderId == orderId);
                    order.PaymentResult = 1;
                    _context.Order.Update(order);
                    _context.SaveChanges();
                    //Thanh toan thanh cong
                    //displayMsg.InnerText = "Thanh toán thành công";
                    HttpContext.Session.Remove("cart");
                    return View(0);
                }
                else
                {
                    //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                    //displayMsg.InnerText = "Có lỗi xảy ra trong quá trình xử lý.Mã lỗi: " + vnp_ResponseCode;
                    HttpContext.Session.Remove("cart");
                    return View(1);
                }
            }
            else
            {
                //log.InfoFormat("Invalid signature, InputData={0}", Request.RawUrl);
                //displayMsg.InnerText = "Có lỗi xảy ra trong quá trình xử lý";
                HttpContext.Session.Remove("cart");
                return View(2);
            }
        }
    }
}
