﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class LoginInputModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
