using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace BankingApp.Utility
{ 
    public class RegisteredUsers
    {
        public static List<User> Users { get; set; }
    }

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double AccountTotal { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Transaction NewTransaction { get; set; }
    }

    public class Transaction
    {
        public double Amount { get; set; }
        public string Type { get; set; }
    }
}
