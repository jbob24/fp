using FinanceProjector.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Interfaces
{
    public interface ITransactionProvider
    {
        List<Transaction> LoadTransactions(FileStream data);
    }
}
