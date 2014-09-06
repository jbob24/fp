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

        public ActionResult Index()
        {
            var service = new TransactionService();
            var user = service.GetUserByUserId("joehenss");
            var model = new BudgetCategoryViewModel(user);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCategory(string userName, string category, string parent)
        {
            var service = new TransactionService();
            var user = service.GetUserByUserId(userName);

            var existingCategory = user.BudgetCategories.FirstOrDefault(c => c.Name.Equals(parent, StringComparison.InvariantCultureIgnoreCase));
            var newCategory = new BudgetCategory { Name = category };

            if (existingCategory != null)
            {
                existingCategory.SubCategories.Add(newCategory);
            }
            else
            {
                user.BudgetCategories.Add(newCategory);
            }

            service.SaveUser(user);

            return PartialView("BudgetCategoryList", user.BudgetCategories);
        }
    }
}
