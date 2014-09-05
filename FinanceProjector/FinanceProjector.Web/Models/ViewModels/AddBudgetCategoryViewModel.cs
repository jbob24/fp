using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinanceProjector.Web.Models.ViewModels
{
    public class AddBudgetCategoryViewModel
    {
        [Required(ErrorMessage="Name is required")]
        public string Name { get; set; }
        public List<string> BudgetCategories { get; set; }

        public AddBudgetCategoryViewModel()
        {
            BudgetCategories = new List<string>();
        }
    }
}