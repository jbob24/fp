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
    public class BudgetController : Controller
    {

        public ActionResult Index()
        {
            var service = new TransactionService();
            var user = service.GetUserByUserId(User.Identity.Name);

            var model = new BudgetListViewModel(user, DateTime.Now);

            return View(model);
        }

    }
}
