using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.TransactionProviders.OFX
{
    public static class OFXConstants
    {
        public const string QFX_START_BLOCK_FORMAT = "<{0}>";
        public const string QFX_END_BLOCK_FORMAT = "</{0}>";
        public const string QFX_TRANSACTION = "STMTTRN";
        public const string QFX_TRANSACTION_TYPE = "TRNTYPE";
        public const string QFX_DATE_POSTED = "DTPOSTED";
        public const string QFX_AMOUNT = "TRNAMT";
        public const string QFX_FI_ID = "FITID";
        public const string QFX_CHECK_NUMBER = "CHECKNUM";
        public const string QFX_MEMO = "MEMO";
        public const string QFX_NAME = "NAME";
        public const string OFX_END_TAG = "OFX>";
    }
}
