using System.Web.Services.Description;
using FinanceProjector.Model;
using FinanceProjector.Services;
using FinanceProjector.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinanceProjector.Web.Controllers
{
    public class BudgetCategoryController : Controller
    {
        private TransactionService _service;

        public BudgetCategoryController()
        {
            _service = new TransactionService();
        }

        public ActionResult Index()
        {
            var user = _service.GetUserByUserId(User.Identity.Name);
            var model = new BudgetCategoryViewModel(user);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCategory(string userName, string category, string parent)
        {
            var user = _service.GetUserByUserId(userName);

            if (!string.IsNullOrWhiteSpace(category))
            {
                var existingCategory =
                    user.BudgetCategories.FirstOrDefault(
                        c => c.Name.Equals(parent, StringComparison.InvariantCultureIgnoreCase));
                var newCategory = new BudgetCategory {Name = category};

                if (existingCategory != null)
                {
                    existingCategory.SubCategories.Add(newCategory);
                }
                else
                {
                    user.BudgetCategories.Add(newCategory);
                }

                _service.SaveUser(user);
            }

            return PartialView("BudgetCategoryList", user.BudgetCategories);
        }

        public ActionResult Edit(string categoryName)
        {
            var user = _service.GetUserByUserId(User.Identity.Name);
            var category = FindBudgetCategory(user.BudgetCategories, categoryName); // user.BudgetCategories.FirstOrDefault(c => c.Name == categoryName);

            if (category != null)
            {
                return View(new EditBudgetCatgegoryViewModel(category));
            }

            return RedirectToAction("Index");
        }

        public ActionResult AddTransactionMatch(TransactionMatchViewModel match)
        {
            var user = _service.GetUserByUserId(User.Identity.Name);

            if (user != null)
            {
                var category = FindBudgetCategory(user.BudgetCategories, match.CategoryName); // user.BudgetCategories.FirstOrDefault(c => c.Name == match.CategoryName);

                if (category != null)
                {
                    category.TransactionMatches.Add(new TransactionMatch { TransactionType = match.TransactionType, Amount = match.Amount, Comments = match.Comments, Name = match.Name, PayeeID = match.PayeeID});

                    _service.SaveUser(user);

                    return PartialView("TransactionMatchList", category.TransactionMatches);
                }
            }

            return null;
        }

        private BudgetCategory FindBudgetCategory(List<BudgetCategory> categories, string categoryName)
        {
            BudgetCategory foundCategory = null;

            foreach (var budgetCategory in categories)
            {
                if (budgetCategory.Name == categoryName)
                {
                    foundCategory = budgetCategory;
                }

                if (foundCategory != null)
                {
                    return foundCategory;
                }

                if (budgetCategory.SubCategories.Any())
                {
                    foundCategory = FindBudgetCategory(budgetCategory.SubCategories, categoryName);
                }

                if (foundCategory != null)
                {
                    return foundCategory;
                }
            }

            return null;
        }
    }
}
