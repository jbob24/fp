using FinanceProjector.Interfaces;
using FinanceProjector.Repository;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Model
{
    public class User : IMongoEntity
    {
        public ObjectId Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Transaction> Transactions { get; set; }
        public List<BudgetCategory> BudgetCategories { get; set; }
        public List<Budget> Budgets { get; set; }
        public string Password { get; set; }
        public string PasswordSecurityQuestion { get; set; }
        public string PasswordSecurityAnswer { get; set; }

        public User()
        {
            Transactions = new List<Transaction>();
            BudgetCategories = new List<BudgetCategory>();
            Budgets = new List<Budget>();
        }

        public BudgetCategory AddBudgetCategory(string name)
        {
            var newCategory = new BudgetCategory(name);
            this.BudgetCategories.Add(newCategory);
            return newCategory;
        }
    }
}
