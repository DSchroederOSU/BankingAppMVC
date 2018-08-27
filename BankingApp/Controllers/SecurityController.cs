using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Models;
using Microsoft.AspNetCore.Authentication;
using BankingApp.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankingApp.Controllers
{
    public class SecurityController : Controller
    { 
        public SecurityController()
        {

        }

        public IActionResult Login()
        { 
            return View();
        }

        public IActionResult Register()
        { 
            return View("./Register");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel inputModel)
        { 
            if (!IsAuthentic(inputModel.Email, inputModel.Password))
            {
                TempData["headermsg"] = "<div id='alertMessage' class='alert alert-danger w-100 h-100' role='alert'>Invalid Credentials.</div> ";
                return View();
            }

            // create claims  
            List<Claim> claims = new List<Claim>

            {
                new Claim(ClaimTypes.Name, inputModel.Email),
                new Claim(ClaimTypes.Email, inputModel.Email)
            };

            // create identity  
            ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

            // create principal  
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // sign-in  
            await HttpContext.SignInAsync(
                    scheme: CookieAuthenticationDefaults.AuthenticationScheme,
                    principal: principal); 
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public  IActionResult Register(RegisterInputModel inputModel, string submit)
        {
            if (RegisterUser(inputModel)){
                TempData["headermsg"] = "<div id='alertMessage' class='alert alert-success w-100 h-100' role='alert'>Registration Successful.</div> ";
                return View("Login");
            }
            else{
                TempData["headermsg"] = "<div id='alertMessage' class='alert alert-danger w-100 h-100' role='alert'>A user with this email already exists.</div> ";
                return View("Login");
            }
        }

        public async Task<IActionResult> Logout()
        { 
            await HttpContext.SignOutAsync(
                    scheme: CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        #region " Private "

        private bool IsAuthentic(string email, string password)
        {
            if(RegisteredUsers.Users.Count > 0){
                if(RegisteredUsers.Users.Find((User obj) => obj.Email == email && obj.Password == password) != null){
                    HttpContext.Session.SetString("CurrentUserEmail", email); 
                    return true;
                }
            } 
            return false;
        }
 
        private bool RegisterUser(RegisterInputModel inputModel){
            if (RegisteredUsers.Users.Count > 0)
            {
                if (RegisteredUsers.Users.Find((User obj) => obj.Email == inputModel.Email) != null)
                {
                    return false;
                }
            }
            User NewUser = new User
            {
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Email = inputModel.Email,
                Password = inputModel.Password,
                AccountTotal = 12000,
                Transactions = new List<Transaction>()
            };
            RegisteredUsers.Users.Add(NewUser);
            return true;
        }

        #endregion
    }
}
