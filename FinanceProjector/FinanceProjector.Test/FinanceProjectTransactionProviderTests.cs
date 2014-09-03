using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceProjector.TransactionProviders;
using System.IO;
using System.Collections.Generic;
using FinanceProjector.TransactionProviders.OFX;
using FinanceProjector.Model;
using System.Linq;
using FinanceProjector.Enums;

namespace FinanceProjector.Test
{
    [TestClass]
    public class FinanceProjectTransactionProviderTests
    {
        private TransactionService _service;
        private static string _userId = "joehenss";
        private User _user;

        [TestInitialize]
        public void Setup()
        {
            _service = new TransactionService();
            _user = CreateUser();
            AddUser();
        }

        [TestCleanup]
        public void TearDown()
        {
            RemoveUser();
        }

        private void AddUser()
        {
            _service.AddUser(_user);
        }

        private static User CreateUser()
        {
            var user = new User();
            user.FirstName = "Joe";
            user.LastName = "Henss";
            user.UserId = _userId;
            return user;
        }

        private void RemoveUser()
        {
            //_service.DeleteUser(_user);
            //var user = _service.GetUserByUserId(_userId);

            //Assert.IsNull(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TransactionService_FilePathNull()
        {
            var transactions = _service.ImportTransactions(null, null, Enums.TransactionProviderType.OFXTransactionProvider);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TransactionService_UserIdNull()
        {
            var transactions = _service.ImportTransactions(null, "blah", Enums.TransactionProviderType.OFXTransactionProvider);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TransactionService_InvalidFile()
        {
            var transactions = _service.ImportTransactions(_user, "blah", Enums.TransactionProviderType.OFXTransactionProvider);
        }

        [TestMethod]
        public void OFXTransactionProvider_Load()
        {
            //var provider = new OFXTransactionProvider();

            SetupBudgetCategories();
            
            var filePath = @"Checking1.qfx";

            var transactions = _service.ImportTransactions(_user, filePath, Enums.TransactionProviderType.OFXTransactionProvider);

            Assert.IsNotNull(transactions);
            Assert.AreEqual(1098, _user.Transactions.Count);

            ValidateTransaction(transactions[0], GetFirstExpectedTransaction());
            ValidateTransaction(transactions[transactions.Count - 1], GetLastExpectedTransaction());

            Remove5Transactions();
        }

        [TestMethod]
        public void SetupBudgetCategories()
        {
            if (_user.BudgetCategories == null || !_user.BudgetCategories.Any())
            {
                var groceries = new BudgetCategory { Name = "Groceries" };
                groceries.TransactionMatches.Add(new TransactionMatch { Name = "Windmill Farms" });
                groceries.TransactionMatches.Add(new TransactionMatch { Name = "VONS Store" });
                groceries.TransactionMatches.Add(new TransactionMatch { Name = "Trader Joe s" });

                var foodCategory = new BudgetCategory { Name = "Food" };
                foodCategory.SubCategories.Add (groceries);
                foodCategory.SubCategories.Add(new BudgetCategory { Name = "Eating Out" });

                var lukie = new BudgetCategory { Name = "Lukie" };
                lukie.TransactionMatches.Add(new TransactionMatch { TransactionType = TransactionType.Check, Amount = -500m });
                var childCare = new BudgetCategory { Name = "Child Care" };
                childCare.SubCategories.Add(lukie);

                var rent = new BudgetCategory { Name = "Rent" };
                rent.TransactionMatches.Add(new TransactionMatch { TransactionType = TransactionType.Check, Amount = -2100m });
                var housing = new BudgetCategory { Name = "Housing" };
                housing.SubCategories.Add(rent);

                _user.BudgetCategories.Add(foodCategory);
                _user.BudgetCategories.Add(childCare);
                _user.BudgetCategories.Add(housing);

                _service.SaveUser(_user);
            }
        }

        private void Remove5Transactions()
        {
            var t1 = _user.Transactions[100].TransactionID;
            var t2 = _user.Transactions[200].TransactionID;
            var t3 = _user.Transactions[700].TransactionID;
            var t4 = _user.Transactions[783].TransactionID;
            var t5 = _user.Transactions[921].TransactionID;

            _user.Transactions.RemoveAll(t => t.TransactionID == t1 || t.TransactionID == t2 || t.TransactionID == t3 || t.TransactionID == t4 || t.TransactionID == t5);

            Assert.AreEqual(1093, _user.Transactions.Count);

            _service.SaveUser(_user);

            var savedUser = _service.GetUserByUserId(_userId);

            Assert.AreEqual(1093, savedUser.Transactions.Count);

            var dt1 = savedUser.Transactions.FirstOrDefault(t => t.TransactionID == t1);

            Assert.IsNull(dt1);
        }

        private Transaction GetFirstExpectedTransaction()
        {
            var transaction = new Transaction();
            transaction.TransactionType = Enums.TransactionType.Check;
            transaction.Date = new DateTime(2014, 01, 02, 12, 0, 0, 0, 0);
            transaction.Amount = -500m;
            transaction.TransactionID = "2014010210";
            transaction.CheckNum = "1506";
            transaction.Name = "CHECK";

            return transaction;

        }


        private Transaction GetLastExpectedTransaction()
        {
            var transaction = new Transaction();
            transaction.TransactionType = Enums.TransactionType.PointOfSale;
            transaction.Date = new DateTime(2014, 08, 20, 12, 0, 0, 0, 0);
            transaction.Amount = -14.94m;
            transaction.TransactionID = "201408201";
            transaction.Name = "Amazon.com";
            transaction.Comments = "CHECK CRD PURCHASE 08/19 AMZN.COM/BILL WA 434257XXXXXX4549 464231178829317 ?MCC=5942";

            return transaction;

        }

        private void ValidateTransaction(Transaction actual, Transaction expected)
        {
            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.CheckNum, actual.CheckNum);
            Assert.AreEqual(expected.Comments, actual.Comments);
            Assert.AreEqual(expected.Currency, actual.Currency);
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.TransactionID, actual.TransactionID);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.PayeeID, actual.PayeeID);
            Assert.AreEqual(expected.ReferenceNumber, actual.ReferenceNumber);
            Assert.AreEqual(expected.TransactionID, actual.TransactionID);
            Assert.AreEqual(expected.TransactionType, actual.TransactionType);
        }
    }
}
