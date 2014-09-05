using FinanceProjector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceProjector.Web.Models.ViewModels
{
    public class BudgetCategoryViewModel
    {
        public User User { get; set; }
        public AddBudgetCategoryViewModel AddBudgetCategory { get; set; }

        public BudgetCategoryViewModel(User user)
        {
            this.User = user;
            AddBudgetCategory = new AddBudgetCategoryViewModel();
            this.User.BudgetCategories.OrderBy(c => c.Name).ToList().ForEach(c => AddBudgetCategory.BudgetCategories.Add(c.Name));
        }
    }
}