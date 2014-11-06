using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Model
{
    public class Account
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }

        // Move Transactions, Budget Categories and Budgets here from User. 
        // Add methods to create account and add users
    }
}
