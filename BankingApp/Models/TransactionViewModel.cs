using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Builder;
using BankingApp.Utility;

namespace BankingApp.Models
{
    public class TransactionViewModel
    {
        public List<Transaction> UserTransactions { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; }
    }
} 