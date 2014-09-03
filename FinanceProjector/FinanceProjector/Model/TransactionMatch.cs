using FinanceProjector.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Model
{
    public class TransactionMatch
    {
        public TransactionType? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public string PayeeID { get; set; }

        public bool IsMatch(Transaction transaction)
        {
            return IsTypeMatch(transaction) && IsAmountMatch(transaction) && IsNameMatch(transaction) && IsCommentsMatch(transaction) && IsPayeeIDMatch(transaction);
        }

        private bool IsTypeMatch(Transaction transaction)
        {
            if (TransactionType.HasValue)
            {
                return TransactionType.Value == transaction.TransactionType;
            }

            return true;
        }

        private bool IsAmountMatch(Transaction transaction)
        {
            if (Amount.HasValue)
            {
                return Amount.Value == transaction.Amount;
            }

            return true;
        }

        private bool IsNameMatch(Transaction transaction)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return !string.IsNullOrEmpty(transaction.Name) && Name.Equals(transaction.Name, StringComparison.InvariantCultureIgnoreCase);
            }

            return true;
        }

        private bool IsCommentsMatch(Transaction transaction)
        {
            if (!string.IsNullOrEmpty(Comments))
            {
                return !string.IsNullOrEmpty(transaction.Comments) && Comments.Equals(transaction.Comments, StringComparison.InvariantCultureIgnoreCase);
            }

            return true;
        }

        private bool IsPayeeIDMatch(Transaction transaction)
        {
            if (!string.IsNullOrEmpty(PayeeID))
            {
                return !string.IsNullOrEmpty(transaction.PayeeID) && PayeeID.Equals(transaction.PayeeID, StringComparison.InvariantCultureIgnoreCase);
            }

            return true;
        }
    }
}
