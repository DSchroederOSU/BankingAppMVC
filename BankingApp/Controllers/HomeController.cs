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
        public static double RunningBalance = 12000.00;
        public HomeController()
        {
        }

        public IActionResult Index( )
        {
            ViewData["RunningBalance"] = RunningBalance;
            Console.WriteLine(HttpContext.Session.GetObjectFromJson<User>("CurrentUser").Transactions.Count);
            var viewModel = new TransactionViewModel()
            {
                UserTransactions = HttpContext.Session.GetObjectFromJson<User>("CurrentUser").Transactions
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Deposit(TransactionViewModel model)
        {
            if (ValidateDeposit(model.Amount))
            {
                User CurrentUser = HttpContext.Session.GetObjectFromJson<User>("CurrentUser");
                Transaction ToAdd = new Transaction
                {
                    Amount = model.Amount,
                    Type = "Deposit"
                };
                CurrentUser.Transactions.Add(ToAdd);
                HttpContext.Session.SetObjectAsJson("CurrentUser", CurrentUser);
                RunningBalance = RunningBalance + ToAdd.Amount;
                ViewData["RunningBalance"] = RunningBalance;
                ModelState.Clear();
                return Redirect(Url.Content("~/Home/Index"));
            }
            else
            {
                ViewData["RunningBalance"] = RunningBalance;
                ModelState.Clear();
                return Redirect(Url.Content("~/Home/Index"));
            }
        }

        [HttpPost]
        public ActionResult Withdraw(TransactionViewModel model)
        {
            if (ValidateWithdraw(model.Amount))
            {
                User CurrentUser = HttpContext.Session.GetObjectFromJson<User>("CurrentUser");
                Transaction ToAdd = new Transaction
                {
                    Amount = model.Amount,
                    Type = "Withdraw"
                };
                CurrentUser.Transactions.Add(ToAdd);
                HttpContext.Session.SetObjectAsJson("CurrentUser", CurrentUser);
                RunningBalance = RunningBalance - ToAdd.Amount;
                ViewData["RunningBalance"] = RunningBalance;
                ModelState.Clear();
                return Redirect(Url.Content("~/Home/Index"));
            }
            else
            {
                ViewData["RunningBalance"] = RunningBalance;
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

        public bool ValidateWithdraw(double value)
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
            else if (RunningBalance - value < 0)
            {
                TempData["errormsg"] = "<div id='alertMessage' class='alert alert-danger w-100 h-100' role='alert'>You do not have enough funds for this withdrawl.</div> ";
                return false;
            }
            TempData["errormsg"] = "<div id='alertMessage' class='alert alert-success w-100 h-100' role='alert'>Withdraw Successful.</div> ";
            return true;
        }
    }
}
