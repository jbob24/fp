using FinanceProjector.Enums;
using FinanceProjector.TransactionProviders.OFX.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.TransactionProviders.OFX.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string Clean(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }

            var temp = str.TrimStart().Split(new[] { '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var cleanLine = String.Join("", temp);

            return cleanLine.Substring(cleanLine.IndexOf('>') + 1);
        }

        public static DateTime ToDateTime(this string str)
        {
            if (str.Length < 18)
            {
                return new DateTime();
            }

            try
            {
                return DateTime.ParseExact(str, "yyyyMMddHHmmss.fff", CultureInfo.InvariantCulture);
            }
            catch
            {
                return new DateTime();
            }
        }

        public static LineType GetLineType(this string line)
        {
            if (line.Contains(string.Format(OFXConstants.QFX_START_BLOCK_FORMAT, OFXConstants.QFX_TRANSACTION_TYPE)))
            {
                return LineType.TransactionType;
            }

            if (line.Contains(string.Format(OFXConstants.QFX_START_BLOCK_FORMAT, OFXConstants.QFX_DATE_POSTED)))
            {
                return LineType.DatePosted;
            }

            if (line.Contains(string.Format(OFXConstants.QFX_START_BLOCK_FORMAT, OFXConstants.QFX_AMOUNT)))
            {
                return LineType.TransactionAmount;
            }

            if (line.Contains(string.Format(OFXConstants.QFX_START_BLOCK_FORMAT, OFXConstants.QFX_FI_ID)))
            {
                return LineType.FITransactionID;
            }

            if (line.Contains(string.Format(OFXConstants.QFX_START_BLOCK_FORMAT, OFXConstants.QFX_CHECK_NUMBER)))
            {
                return LineType.CheckNumber;
            }

            if (line.Contains(string.Format(OFXConstants.QFX_START_BLOCK_FORMAT, OFXConstants.QFX_NAME)))
            {
                return LineType.Name;
            }

            if (line.Contains(string.Format(OFXConstants.QFX_START_BLOCK_FORMAT, OFXConstants.QFX_MEMO)))
            {
                return LineType.Memo;
            }


            return LineType.Unknown;
        }

        public static TransactionType ToTransactionType(this string str)
        {
            switch (str)
            {
                case "CREDIT":
                    return TransactionType.Credit;
                case "DEBIT":
                    return TransactionType.Debit;
                case "INT":
                    return TransactionType.Interest;
                case "DIV":
                    return TransactionType.Dividend;
                case "FEE":
                    return TransactionType.Fee;
                case "SRVCHG":
                    return TransactionType.ServiceCharge;
                case "DEP":
                    return TransactionType.Deposit;
                case "ATM":
                    return TransactionType.ATMTransfer;
                case "POS":
                    return TransactionType.PointOfSale;
                case "XFER":
                    return TransactionType.Transfer;
                case "CHECK":
                    return TransactionType.Check;
                case "PAYMENT":
                    return TransactionType.Payment;
                case "CASH":
                    return TransactionType.CashWithdrawl;
                case "DIRECTDEP":
                    return TransactionType.DirectDeposit;
                case "DIRECTDEBIT":
                    return TransactionType.DirectDebit;
                case "REPEATPMT":
                    return TransactionType.RepeatingAmount;
                default:
                    return TransactionType.Other;
            }
        }
    } 
}
