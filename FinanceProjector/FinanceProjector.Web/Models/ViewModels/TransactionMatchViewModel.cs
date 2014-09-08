using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinanceProjector.Enums;

namespace FinanceProjector.Web.Models.ViewModels
{
    public class TransactionMatchViewModel
    {
        public string CategoryName { get; set; }
        public TransactionType? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public string PayeeID { get; set; }
    }
}