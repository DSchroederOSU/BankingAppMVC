using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;
using BankingApp.Utility;

namespace BankingApp.Controllers
{

    [Authorize]
    public class HomeController : Controller
    { 

        public HomeController()
        {
        }

        public IActionResult Index(string email)
        {
            Console.WriteLine(email);
            Console.WriteLine(HttpContext.Session.GetObjectFromJson<List<User>>("RegisteredUsers")[0].Email);
            Console.WriteLine(HttpContext.Session.GetObjectFromJson<List<User>>("RegisteredUsers")
                .Find((User obj) => obj.Email == email)); 
            return View();
        }
    }
}
