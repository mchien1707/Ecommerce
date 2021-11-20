using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectTraining.Models;

namespace ProjectTraining.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly ProjectTrainingContext _context;
        public LoginController(ProjectTrainingContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            var userAdmin = HttpContext.Session.GetString("AdminLogin");
            if(userAdmin != null)
            {
                return Redirect("~/Admin/");
            }
            return View();
        }
        [HttpPost]
        public IActionResult HandleLogin(string Email, string Password)
        {
            var user = _context.User.SingleOrDefault(x => x.Email == Email);
            if(user != null)
            {
                bool check = BCrypt.Net.BCrypt.Verify(Password, user.Password);
                
                if (check)
                {
                    UserInfo userInfo = new UserInfo()
                    {
                        UserId = user.UserId,
                        LastName = user.LastName,
                        Email = user.Email,
                        Role = user.Role,
                    };
                    if (user.Role == "Admin")
                    {
                        HttpContext.Session.SetString("AdminLogin", JsonConvert.SerializeObject(userInfo));
                        return Redirect("~/Admin/Products");
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult HandleLogout()
        {
            HttpContext.Session.Remove("AdminLogin");
            return RedirectToAction(nameof(Index));
        }

    }
}
