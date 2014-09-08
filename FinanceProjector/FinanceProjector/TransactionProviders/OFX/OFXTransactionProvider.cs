using FinanceProjector.Enums;
using FinanceProjector.Interfaces;
using FinanceProjector.Model;
using FinanceProjector.TransactionProviders.OFX.Enums;
using FinanceProjector.Utilities;
using FinanceProjector.TransactionProviders.OFX.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FinanceProjector.TransactionProviders.OFX
{
    public class OFXTransactionProvider : ITransactionProvider
    {
        public List<Transaction> LoadTransactions(FileStream stream)
        {
            var transactions = new List<Transaction>();

            using (var reader = new StreamReader(stream))
            {
                transactions = LoadTransaction(reader);
            }

            return transactions;
        }

        public List<Transaction> LoadTransactions(string transactionString)
        {
            var transactions = new List<Transaction>();
            string line = string.Empty;

            using (var reader = new StringReader(transactionString))
            {
                line = reader.ReadLine();

                while (line != null && !IsEndOfFile(line))
                {

                    if (IsTransactionStart(line))
                    {
                        transactions.Add(GetTransactionFromBlock(reader));
                    }

                    line = reader.ReadLine();
                }


            }

            return transactions;
        }

        public List<Transaction> LoadTransaction(StreamReader reader)
        {
            var transactions = new List<Transaction>();
            string line = string.Empty;

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();

                if (IsTransactionStart(line))
                {
                    transactions.Add(GetTransactionFromBlock(reader));
                }
            }

            return transactions;
        }

        private Transaction GetTransactionFromBlock(StringReader reader)
        {
            var line = reader.ReadLine();
            var transaction = new Transaction();

            while (!IsTransactionend(line) && !IsEndOfFile(line))
            {
                var cleanedLine = line.Clean();

                switch (line.GetLineType())
                {
                    case LineType.CheckNumber:
                        transaction.CheckNum = cleanedLine;
                        break;
                    case LineType.FITransactionID:
                        transaction.TransactionID = cleanedLine;
                        break;
                    case LineType.Memo:
                        transaction.Comments = cleanedLine;
                        break;
                    case LineType.Name:
                        transaction.Name = cleanedLine;
                        break;
                    case LineType.DatePosted:
                        transaction.Date = cleanedLine.ToDateTime();
                        break;
                    case LineType.TransactionAmount:
                        transaction.Amount = cleanedLine.ToDecimal();
                        break;
                    case LineType.TransactionType:
                        transaction.TransactionType = cleanedLine.ToTransactionType();
                        break;
                    default:
                        throw new Exception(string.Format("Line type not accounted for - {0}", line));
                        break;
                }

                line = reader.ReadLine();
            }

            return transaction;
        }

        private Transaction GetTransactionFromBlock(StreamReader reader)
        {
            var line = reader.ReadLine();
            var transaction = new Transaction();

            while (!IsTransactionend(line) && !reader.EndOfStream)
            {
                var cleanedLine = line.Clean();

                switch (line.GetLineType())
                {
                    case LineType.CheckNumber:
                        transaction.CheckNum = cleanedLine;
                        break;
                    case LineType.FITransactionID:
                        transaction.TransactionID = cleanedLine;
                        break;
                    case LineType.Memo:
                        transaction.Comments = cleanedLine;
                        break;
                    case LineType.Name:
                        transaction.Name = cleanedLine;
                        break;
                    case LineType.DatePosted:
                        transaction.Date = cleanedLine.ToDateTime();
                        break;
                    case LineType.TransactionAmount:
                        transaction.Amount = cleanedLine.ToDecimal();
                        break;
                    case LineType.TransactionType:
                        transaction.TransactionType = cleanedLine.ToTransactionType();
                        break;
                    default:
                        throw new Exception(string.Format("Line type not accounted for - {0}", line));
                        break;
                }

                line = reader.ReadLine();
            }

            return transaction;
        }

        private bool IsTransactionStart(string line)
        {
            return line.Contains(string.Format(OFXConstants.QFX_START_BLOCK_FORMAT, OFXConstants.QFX_TRANSACTION));
        }

        private bool IsTransactionend(string line)
        {
            return line.Contains(string.Format(OFXConstants.QFX_END_BLOCK_FORMAT, OFXConstants.QFX_TRANSACTION));
        }

        private bool IsEndOfFile(string line)
        {
            return line.Contains(string.Format(OFXConstants.QFX_END_BLOCK_FORMAT, OFXConstants.OFX_END_TAG));
        }
    }
}
