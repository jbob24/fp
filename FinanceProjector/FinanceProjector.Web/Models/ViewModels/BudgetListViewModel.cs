using FinanceProjector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceProjector.Web.Models.ViewModels
{
    public class BudgetListViewModel
    {
        public Budget Budget { get; set; }
        public List<BudgetListItem> BudgetItems { get; set; }

        public BudgetListViewModel(User user, DateTime month)
        {
            var budget = user.Budgets.FirstOrDefault(b => b.Month.Year == DateTime.Now.Year && b.Month.Month == DateTime.Now.Month);

            if (budget == null)
            {
                budget = Budget.Create(DateTime.Now);
            }

            this.Budget = budget;
            this.BudgetItems = new List<BudgetListItem>();

            FillActuals(user);

            foreach (var category in user.BudgetCategories)
            {
                var plannedItem = this.Budget.PlannedItems.FirstOrDefault(i => i.CategoryName == category.Name);
                var actualItem = this.Budget.ActualItems.FirstOrDefault(i => i.CategoryName == category.Name);
                var budgetItem = new BudgetListItem();

                budgetItem.CategoryName = category.Name;

                if (plannedItem != null)
                {
                    budgetItem.PlannedCredit = plannedItem.CreditAmount;
                    budgetItem.PlannedDebit = plannedItem.DebitAmount;
                }

                if (actualItem != null)
                {
                    budgetItem.ActualCredit = actualItem.CreditAmount;
                    budgetItem.ActualDebit = actualItem.DebitAmount;
                }

                budgetItem.SubCategories = new List<BudgetListItem>();

                //foreach (var subCategory in category.SubCategories)
                //{
                //    plannedItem = this.Budget.PlannedItems.FirstOrDefault(i => i.CategoryName == subCategory.Name);
                //    actualItem = this.Budget.ActualItems.FirstOrDefault(i => i.CategoryName == subCategory.Name);
                //    var budgetSubItem = new BudgetListItem();

                //    budgetSubItem.CategoryName = subCategory.Name;

                //    if (plannedItem != null)
                //    {
                //        budgetSubItem.PlannedCredit = plannedItem.CreditAmount;
                //        budgetSubItem.PlannedDebit = plannedItem.DebitAmount;
                //    }

                //    if (actualItem != null)
                //    {
                //        budgetSubItem.ActualCredit = actualItem.CreditAmount;
                //        budgetSubItem.ActualDebit = actualItem.DebitAmount;
                //    }

                //    budgetItem.SubCategories.Add(budgetSubItem);
                //}

                this.BudgetItems.Add(budgetItem);
            }
        }

        private void FillActuals(User user)
        {
            var month = this.Budget.Month;
            var transactions = user.Transactions.Where(t => t.Date.Year == month.Year && t.Date.Month == month.Month && !string.IsNullOrEmpty(t.BudgetCategoryName));

            foreach (var transaction in transactions)
            {
                var actualItem = this.Budget.ActualItems.FirstOrDefault(i => i.CategoryName == transaction.BudgetCategoryName);

                if (actualItem == null)
                {
                    actualItem = new BudgetItem();
                    actualItem.CategoryName = transaction.BudgetCategoryName;
                    this.Budget.ActualItems.Add(actualItem);
                }

                if (transaction.Amount > 0)
                    actualItem.CreditAmount += transaction.Amount;
                else
                    actualItem.DebitAmount += -transaction.Amount;                
            }
        }
    }

    public class BudgetListItem
    {
        public string CategoryName { get; set; }
        public decimal PlannedCredit { get; set; }
        public decimal PlannedDebit { get; set; }
        public decimal PlannedTotal { get; set; }
        public decimal PlannedPctOfBudget { get; set; }

        public decimal ActualCredit { get; set; }
        public decimal ActualDebit { get; set; }
        public decimal ActualTotal { get; set; }
        public decimal ActualPctOfBudget { get; set; }

        public List<BudgetListItem> SubCategories { get; set; }
    }
}