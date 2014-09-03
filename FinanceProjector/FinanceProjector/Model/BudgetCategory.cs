using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Model
{
    public class BudgetCategory
    {
        public string Name { get; set; }
        public List<TransactionMatch> TransactionMatches { get; set; }
        public List<BudgetCategory> SubCategories { get; set; }

        public BudgetCategory()
        {
            SubCategories = new List<BudgetCategory>();
            TransactionMatches = new List<TransactionMatch>();
        }
    }
}
