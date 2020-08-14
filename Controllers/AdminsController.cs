using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksMagazine.Data;
using BooksMagazine.Models;

namespace BooksMagazine.Controllers
{
    public class AdminsController : Controller
    {
        private readonly BooksMagazineContext _context;

        public AdminsController(BooksMagazineContext context)
        {
            _context = context;
        }

        // GET: Admins/AddAdmin
        public IActionResult AddAdmin(string token)
        {
            var admin = new Admin()
            {
                Token = token
            };
            return View(admin);
        }

        // POST: Admins/AddAdmin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(Admin admin)
        {
            
            _context.Add(admin);
            await _context.SaveChangesAsync();
            return Redirect("/Admins/Login");
        }
        // GET: Admins/Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Admin admin)
        {
            if (admin == null)
            {
                return NotFound();
            }
            var item = await _context.Admin.FirstOrDefaultAsync(a => a.UserName == admin.UserName);
            
            if(item !=null)
            {
                if (admin.Password == item.Password && admin.Token == item.Token)
                {
                    Helpers.Constants.Admin = item;
                }
            }
            return Redirect("/home");
        }
        // GET: /Admins/GetToken
        public IActionResult GetToken()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetToken(TokenParamater tokenParamater)
        {
            var str = GenerateToken(tokenParamater);
            return Redirect($"/Admins/AddAdmin/?token={str}");
        }
        public IActionResult LogOut()
        {
            Helpers.Constants.Admin = null;
            return Redirect("/Home");
        }
        string str0; string str1;  
        private string GenerateToken(TokenParamater tokenParamater)
        {
            foreach (var item in tokenParamater.UserName)
            {
                var r = new Random();
                str0 += r.Next(9) + item + r.Next(9) + item + r.Next(9) + item;
            }
            foreach (var item in tokenParamater.Birthaday)
            {
                var r = new Random();
                for (int i = 0; i < 3; i++)
                {
                    str1 += r.Next(9);
                }
                str1 += item;
            }

            return str0 + str1;
        }
    }
}
