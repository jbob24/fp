using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinanceProjector.Enums;
using FinanceProjector.Services;

namespace FinanceProjector.Web.Controllers
{
    public class TransactionController : Controller
    {
        private TransactionService _service;

        public TransactionController()
        {
            _service = new TransactionService();
        }

        public ActionResult Index()
        {
            var service = new TransactionService();
            var user = service.GetUserByUserId(User.Identity.Name);

            //user.Transactions.Clear();
            //service.SaveUser(user);

            //user = service.GetUserByUserId(User.Identity.Name);

            return View(user);
        }

        [HttpPost]
        public ActionResult ImportFile(string username, string file)
        {
            var service = new TransactionService();
            var user = service.GetUserByUserId(username);

            var base64EncodedBytes = System.Convert.FromBase64String(file);
            service.ImportTransactions(user, System.Text.Encoding.UTF8.GetString(base64EncodedBytes), TransactionProviderType.OFXTransactionProvider);

            user = service.GetUserByUserId(username);

            return PartialView("TransactionList", user.Transactions);
        }

        [HttpPost]
        public ActionResult ClearAllTransactions()
        {
            var user = _service.GetUserByUserId(User.Identity.Name);
            user.Transactions.Clear();
            _service.SaveUser(user);

            return RedirectToAction("Index");
        }
    }
}