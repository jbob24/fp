using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.TransactionProviders.OFX.Enums
{
    public enum LineType
    {
        Unknown,
        TransactionType,
        DatePosted,
        TransactionAmount,
        FITransactionID,
        CheckNumber,
        Name,
        Memo
    }
}
