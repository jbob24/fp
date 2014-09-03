using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceProjector.TransactionProviders.OFX.ExtensionMethods;

namespace FinanceProjector.Test
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void ToDateTime_Invalid_Date()
        {
            var invalidDateLessThan18Chars = "jkljflksaj";
            var invalidDate18Chars = "jklfdsjklfdsjklfds";
            var dateTimeLessThan18Chars = invalidDateLessThan18Chars.ToDateTime();
            var dateTime18Chars = invalidDate18Chars.ToDateTime();

            Assert.AreEqual(DateTime.MinValue, dateTimeLessThan18Chars);
            Assert.AreEqual(DateTime.MinValue, dateTime18Chars);
        }

        [TestMethod]
        public void ToDateTime_Valid_Dates()
        {
            var validDate1 = "20140102120000.000";
            var validDate2 = "20140820120000.000";
            var validDate3 = "20140421120000.000";
            var validDate4 = "20140421182212.063";

            var date1 = validDate1.ToDateTime();
            var date2 = validDate2.ToDateTime();
            var date3 = validDate3.ToDateTime();
            var date4 = validDate4.ToDateTime();

            var expectedDate1 = new DateTime(2014, 1, 2, 12, 0, 0, 0);
            var expectedDate2 = new DateTime(2014, 8, 20, 12, 0, 0, 0);
            var expectedDate3 = new DateTime(2014, 4, 21, 12, 0, 0, 0);
            var expectedDate4 = new DateTime(2014, 4, 21, 18, 22, 12, 63);

            Assert.AreEqual(expectedDate1, date1);
            Assert.AreEqual(expectedDate2, date2);
            Assert.AreEqual(expectedDate3, date3);
            Assert.AreEqual(expectedDate4, date4);

        }
    }
}
