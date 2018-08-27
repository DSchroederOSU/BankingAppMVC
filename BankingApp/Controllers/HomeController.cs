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
        public static string CurrentEmail;

        public IActionResult Index( )
        {
            CurrentEmail = HttpContext.Session.GetString("CurrentUserEmail");
            Console.WriteLine(CurrentEmail);
            var viewModel = new TransactionViewModel()
            {
                UserTransactions = RegisteredUsers.Users.Find((User obj) => obj.Email == CurrentEmail).Transactions
            };
            ViewData["RunningBalance"] = RegisteredUsers.Users.Find((User obj) => obj.Email == CurrentEmail).AccountTotal;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Deposit(TransactionViewModel model)
        {
            if (ValidateDeposit(model.Amount))
            {
                User current = RegisteredUsers.Users.Find((User obj) => obj.Email == CurrentEmail);
                Console.WriteLine(current.FirstName);
                Transaction ToAdd = new Transaction
                {
                    Amount = model.Amount,
                    Type = "Deposit"
                };
                current.Transactions.Add(ToAdd);
                current.AccountTotal = current.AccountTotal + ToAdd.Amount;  
                ModelState.Clear();
                return Redirect(Url.Content("~/Home/Index"));
            }
            else
            { 
                ModelState.Clear();
                return Redirect(Url.Content("~/Home/Index"));
            }
        }

        [HttpPost]
        public ActionResult Withdraw(TransactionViewModel model)
        {
            User current = RegisteredUsers.Users.Find((User obj) => obj.Email == CurrentEmail);
            if (ValidateWithdraw(model.Amount, current.AccountTotal))
            {
               
                Transaction ToAdd = new Transaction
                {
                    Amount = model.Amount,
                    Type = "Withdraw"
                };
                current.Transactions.Add(ToAdd);
                current.AccountTotal = current.AccountTotal - ToAdd.Amount;
                ModelState.Clear();
                return Redirect(Url.Content("~/Home/Index"));
            }
            else
            { 
                ModelState.Clear();
                return Redirect(Url.Content("~/Home/Index"));
            }
        }

        public bool ValidateDeposit(double value)
        {
            if (value < 0)
            {
                TempData["errormsg"] = "<div id='alertMessage' class='alert alert-danger w-100 h-100' role='alert'>Please enter a positive value.</div> ";
                return false;
            }
            else if (Math.Abs(value) < 0.001)
            {
                TempData["errormsg"] = "<div id='alertMessage' class='alert alert-danger w-100 h-100' role='alert'>You cannot deposit a value of 0.</div> ";
                return false;
            }
            TempData["errormsg"] = "<div id='alertMessage' class='alert alert-success w-100 h-100' role='alert'>Deposit Successful.</div> ";
            return true;
        }

        public bool ValidateWithdraw(double value, double total)
        {
            if (value < 0)
            {
                TempData["errormsg"] = "<div id='alertMessage' class='alert alert-danger w-100 h-100' role='alert'>Please enter a positive value.</div> ";
                return false;
            }
            else if (Math.Abs(value) < 0.001)
            {
                TempData["errormsg"] = "<div id='alertMessage' class='alert alert-danger w-100 h-100' role='alert'>You cannot withdraw a value of 0.</div> ";
                return false;
            }
            else if (total - value < 0)
            {
                TempData["errormsg"] = "<div id='alertMessage' class='alert alert-danger w-100 h-100' role='alert'>You do not have enough funds for this withdrawl.</div> ";
                return false;
            }
            TempData["errormsg"] = "<div id='alertMessage' class='alert alert-success w-100 h-100' role='alert'>Withdraw Successful.</div> ";
            return true;
        }
    }
}
