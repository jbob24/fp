using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FinanceProjector.Model;

namespace FinanceProjector.Web.Models.ViewModels
{
    public class EditBudgetCatgegoryViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public BudgetCategory Category { get; set; }
        public TransactionMatchViewModel TransactionMatch { get; set; }

        public EditBudgetCatgegoryViewModel(BudgetCategory category)
        {
            Category = category;
            TransactionMatch = new TransactionMatchViewModel();
        }
    }
}