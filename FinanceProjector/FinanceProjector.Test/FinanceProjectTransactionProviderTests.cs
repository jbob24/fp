using System;
using FinanceProjector.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceProjector.TransactionProviders;
using System.IO;
using System.Collections.Generic;
using FinanceProjector.TransactionProviders.OFX;
using FinanceProjector.Model;
using System.Linq;
using FinanceProjector.Enums;
using FinanceProjector.Domain.Services;

namespace FinanceProjector.Test
{
    [TestClass]
    public class FinanceProjectTransactionProviderTests
    {
        private TransactionService _service;
        private static string _userId = "joehenss";
        private User _user;
        private bool _deleteData = false;

        [TestInitialize]
        public void Setup()
        {
            _service = new TransactionService();

            _user = _service.GetUserByUserId(_userId);

            if (_user == null)
            {
                _user = CreateUser();
                AddUser();
            }
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
            user.UserName = _userId;
            user.Password = Hasher.Hash("password");
            return user;
        }

        private void RemoveUser()
        {
            if (_deleteData)
            {
                _service.DeleteUser(_user);

                //var user = _service.GetUserByUserId(_userId);
                //Assert.IsNull(user);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TransactionService_FilePathNull()
        {
            var transactions = _service.ImportTransactionsFromFile(null, null, Enums.TransactionProviderType.OFXTransactionProvider);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TransactionService_UserIdNull()
        {
            var transactions = _service.ImportTransactionsFromFile(null, "blah", Enums.TransactionProviderType.OFXTransactionProvider);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void TransactionService_InvalidFile()
        {
            var transactions = _service.ImportTransactionsFromFile(_user, "blah", Enums.TransactionProviderType.OFXTransactionProvider);
        }

        [TestMethod]
        public void OFXTransactionProvider_Load()
        {
            //var provider = new OFXTransactionProvider();

            SetupBudgetCategories();

            var filePath = @"Checking1.qfx";

            var transactions = _service.ImportTransactionsFromFile(_user, filePath, Enums.TransactionProviderType.OFXTransactionProvider);

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
                var income = _user.AddBudgetCategory("Income");
                var incomeItem = income.AddCategoryItem("Income");

                var housing = _user.AddBudgetCategory("Housing");
                var rent = housing.AddCategoryItem("Rent");
                rent.TransactionMatches.Add(new TransactionMatch { TransactionType = TransactionType.Check, Amount = -2100m });

                var utilities = _user.AddBudgetCategory("Utilities");
                var gasElectric = utilities.AddCategoryItem("Gas/Electric");
                var phone = utilities.AddCategoryItem("Phone");
                var internet = utilities.AddCategoryItem("Internet");
                var cable = utilities.AddCategoryItem("Cable");

                var food = _user.AddBudgetCategory("Food");
                var groceries = food.AddCategoryItem("Groceries");
                var eatingOut = food.AddCategoryItem("Eating Out");

                groceries.TransactionMatches.Add(new TransactionMatch { Name = "Windmill Farms" });
                groceries.TransactionMatches.Add(new TransactionMatch { Name = "VONS Store" });
                groceries.TransactionMatches.Add(new TransactionMatch { Name = "Trader Joe s" });

                var clothing = _user.AddBudgetCategory("Clothing");
                clothing.AddCategoryItem("Adults");
                clothing.AddCategoryItem("Children");
                clothing.AddCategoryItem("Cleaners");

                var transportation = _user.AddBudgetCategory("Transportation");
                transportation.AddCategoryItem("Gas & Oil");
                transportation.AddCategoryItem("Repairs & Tires");
                transportation.AddCategoryItem("License & Taxes");
                transportation.AddCategoryItem("Car Replacement");
                transportation.AddCategoryItem("Other");
                transportation.AddCategoryItem("Car Wash");

                var medicalHealth = _user.AddBudgetCategory("Medical/Health");
                medicalHealth.AddCategoryItem("Medications");
                medicalHealth.AddCategoryItem("Doctor Bills");
                medicalHealth.AddCategoryItem("Dentist");
                medicalHealth.AddCategoryItem("Optometrist");
                medicalHealth.AddCategoryItem("Vitamins");
                medicalHealth.AddCategoryItem("Other");

                var insurance = _user.AddBudgetCategory("Insurance");
                insurance.AddCategoryItem("Life");
                insurance.AddCategoryItem("Health");
                insurance.AddCategoryItem("Renters");
                insurance.AddCategoryItem("Auto");

                var personal = _user.AddBudgetCategory("Personal");
                var nanny = personal.AddCategoryItem("Nanny");
                personal.AddCategoryItem("Child Care");
                nanny.TransactionMatches.Add(new TransactionMatch { TransactionType = TransactionType.Check, Amount = -500m });

                personal.AddCategoryItem("Toiletries");
                personal.AddCategoryItem("Cosmetics/Hair Care");
                personal.AddCategoryItem("Preschool");
                personal.AddCategoryItem("Books/Supplies");
                personal.AddCategoryItem("Subscriptions");
                personal.AddCategoryItem("Organization Dues");
                personal.AddCategoryItem("Pocket Money (Melissa)");
                personal.AddCategoryItem("Pocket Money (Joe)");
                personal.AddCategoryItem("Gifts");
                personal.AddCategoryItem("Sports");
                personal.AddCategoryItem("Misc");
                personal.AddCategoryItem("YMCA");
                personal.AddCategoryItem("Household Supplies");
                personal.AddCategoryItem("Nursery");
                personal.AddCategoryItem("School");
                personal.AddCategoryItem("Charity");

                var recreation = _user.AddBudgetCategory("Recreation");
                recreation.AddCategoryItem("Entertainment");
                recreation.AddCategoryItem("Vacation");

                var debts = _user.AddBudgetCategory("Debts");
                debts.AddCategoryItem("Mazda");
                debts.AddCategoryItem("Nissan");
                debts.AddCategoryItem("Dreyfuss");
                debts.AddCategoryItem("Gloria");
                debts.AddCategoryItem("Melissa Chase");
                debts.AddCategoryItem("Citi");
                debts.AddCategoryItem("Bloomingdales");
                debts.AddCategoryItem("Student Loan");
                debts.AddCategoryItem("Banana Republic");

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
