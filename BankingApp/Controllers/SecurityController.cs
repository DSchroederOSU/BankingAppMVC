using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Models;
using Microsoft.AspNetCore.Authentication;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankingApp.Controllers
{
    public class SecurityController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel inputModel)
        {
            if (!IsAuthentic(inputModel.Username, inputModel.Password))
                return View();

            // create claims  
            List<Claim> claims = new List<Claim>
            {
              new Claim(ClaimTypes.Name, "Sean Connery"),
              new Claim(ClaimTypes.Email, inputModel.Username)
            };

            // create identity  
            ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

            // create principal  
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // sign-in  
            await HttpContext.SignInAsync(
                    scheme: "FiverSecurityScheme",
                    principal: principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                    scheme: "FiverSecurityScheme");

            return RedirectToAction("Login");
        }

        #region " Private "

        private bool IsAuthentic(string username, string password)
        {
            return (username == "james" && password == "bond");
        }

        #endregion
    }
}
