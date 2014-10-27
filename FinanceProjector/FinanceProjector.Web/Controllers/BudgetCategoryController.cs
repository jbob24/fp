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
    [Authorize]
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
        public ActionResult AddCategory(string userName, string category)
        {
            var user = _service.GetUserByUserId(userName);

            if (!string.IsNullOrWhiteSpace(category))
            {
                if (!user.BudgetCategories.Any(c => c.Name == category))
                {
                    user.AddBudgetCategory(category);
                    _service.SaveUser(user);
                }
            }

            return PartialView("BudgetCategoryList", user.BudgetCategories);
        }

        public ActionResult Edit(string categoryName)
        {
            var user = _service.GetUserByUserId(User.Identity.Name);
            var category = user.BudgetCategories.FirstOrDefault(c => c.Name == categoryName);

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
                var category = user.BudgetCategories.FirstOrDefault(c => c.Name == match.CategoryName);

                if (category != null)
                {
                    var categoryItem = category.CategoryItems.FirstOrDefault(i => i.Name == match.CategoryItem);

                    if (categoryItem != null)
                    {
                        categoryItem.TransactionMatches.Add(new TransactionMatch { TransactionType = match.TransactionType, Amount = match.Amount, Comments = match.Comments, Name = match.Name, PayeeID = match.PayeeID });
                        _service.SaveUser(user);
                    }


                    return PartialView("TransactionMatchList", categoryItem.TransactionMatches);
                }
            }

            return null;
        }
    }
}
