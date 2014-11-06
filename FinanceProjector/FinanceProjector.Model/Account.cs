using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Model
{
    public class Account
    {
        public int ID { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<Group> CategoryGroups { get; set; }
    }
}
