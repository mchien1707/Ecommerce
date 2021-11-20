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

namespace ProjectTraining.Views.Home
{
    public class UsersController : Controller
    {
        private readonly ProjectTrainingContext _context;

        public UsersController(ProjectTrainingContext context)
        {
            _context = context;
        }

        // GET: Users
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult HandleLogin(User user)
        {
            var userdb = _context.User.SingleOrDefault(x => x.Email == user.Email);
            if(userdb == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                UserInfo userInfo = new UserInfo()
                {
                    UserId = userdb.UserId,
                    Email = userdb.Email,
                    LastName = userdb.LastName,
                    Role = userdb.Role,
                };
                if(BCrypt.Net.BCrypt.Verify(user.Password, userdb.Password) == true)
                {
                    HttpContext.Session.SetString("userLogin", JsonConvert.SerializeObject(userInfo));
                    return Redirect("~/Home");
                }
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: Users/Create
        public IActionResult SignUp()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public IActionResult SignUp(User user)
        {
            var userCheck = _context.User.SingleOrDefault(x => x.Email == user.Email);
            if(userCheck != null)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                User newUser = new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    Phone = user.Phone,
                    Address = user.Address,
                    Role = "Khách hàng",
                };
                _context.Add(newUser);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult HandleLogout()
        {
            HttpContext.Session.Remove("userLogin");
            return RedirectToAction(nameof(Index));
        }
    }
}
