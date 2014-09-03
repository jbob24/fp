using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Enums
{
    public enum TransactionType
    {
        [Description("Basic Credit")]
        Credit,
        [Description("Basic Debit")]
        Debit,
        [Description("Interest")]
        Interest,
        [Description("Dividend")]
        Dividend,
        [Description("Fee")]
        Fee,
        [Description("Service Charge")]
        ServiceCharge,
        [Description("Deposit")]
        Deposit,
        [Description("ATM transfer")]
        ATMTransfer,
        [Description("Point of Sale transfer")]
        PointOfSale,
        [Description("Transfer")]
        Transfer,
        [Description("Check")]
        Check,
        [Description("Payment")]
        Payment,
        [Description("Cash Withdrawal")]
        CashWithdrawl,
        [Description("Direct Deposit")]
        DirectDeposit,
        [Description("Merchant Initiated Debit")]
        DirectDebit,
        [Description("Repeating Payment")]
        RepeatingAmount,
        Other,
    }
}
