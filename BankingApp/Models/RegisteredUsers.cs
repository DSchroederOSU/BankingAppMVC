using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Builder;

namespace BankingApp.Models
{
    public class LoggedUsers
    {
        public List<LoggedUser> Users { get; set; }
    }

    public class LoggedUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
