using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceProjector.Model;
using System.Linq;
using FinanceProjector.Utilities;

namespace FinanceProjector.Test
{
    [TestClass]
    public class RecurringTransactionTest
    {
        [TestMethod]
        public void GetNextRecurrences_FutureDate_ValidDate()
        {
            var recurringTransaction = new RecurringTransaction(new DateTime(2014, 12, 1), 3, "test", 100);

            var firstRecurrence = recurringTransaction.GetNextRecurrences(3).FirstOrDefault();

            Assert.AreEqual(recurringTransaction.StartDate, firstRecurrence);
        }

        [TestMethod]
        public void GetNextRecurrences_PastDateWith5MonthRecurrence_ValidDate()
        {
            var recurringTransaction = new RecurringTransaction(DateTime.Today.AddMonths(-32), 5, "test", 100);
            var expectedDate = DateTime.Today.AddMonths(3).ToFirstOfMonth();

            var firstRecurrence = recurringTransaction.GetNextRecurrences(3).FirstOrDefault();

            Assert.AreEqual(expectedDate, firstRecurrence);
        }

        [TestMethod]
        public void GetNextRecurrences_PastDateWith2MonthRecurrence_ValidDate()
        {
            var recurringTransaction = new RecurringTransaction(DateTime.Today.AddMonths(-32), 2, "test", 100);
            var expectedDate = DateTime.Today.AddMonths(3).ToFirstOfMonth();


            var firstRecurrence = recurringTransaction.GetNextRecurrences(3).FirstOrDefault();

            Assert.AreEqual(expectedDate, firstRecurrence);
        }
    }
}
