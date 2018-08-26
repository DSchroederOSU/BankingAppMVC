using System;
using System.ComponentModel.DataAnnotations;

namespace BankingApp.Models
{
    public class RegisterInputModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
