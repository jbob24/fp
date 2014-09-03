using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinanceProjector.Web.Controllers
{
    public class TransactionController : Controller
    {
        public ActionResult Index()
        {
            var service = new TransactionService();
            var user = service.GetUserByUserId("joehenss");
            return View(user);
        }

    }
}
