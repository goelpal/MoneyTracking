using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracking
{
    public class Transaction
    {
        public Transaction()
        {
        }

        public Transaction(string title, string type, string month, float amount) 
        {
                Title = title;
                Type = type;
                Month = month;
                Amount = amount;
        }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Month { get; set; }
        public float Amount { get; set; }
    }
}

