using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector.Model
{
    public class BudgetCategory
    {
        //public ObjectId Id { get; set; }
        public string Name { get; set; }
        //public List<TransactionMatch> TransactionMatches { get; set; }
        //public List<BudgetCategory> SubCategories { get; set; }
        public List<BudgetCategoryItem> CategoryItems { get; set; }

        public BudgetCategory()
        {
            //SubCategories = new List<BudgetCategory>();
            //TransactionMatches = new List<TransactionMatch>();
            CategoryItems = new List<BudgetCategoryItem>();
        }

        public BudgetCategory(string name) : this()
        {
            this.Name = name;
        }

        public BudgetCategoryItem AddCategoryItem(string name)
        {
            var newItem = new BudgetCategoryItem(name, this.Name);
            CategoryItems.Add(newItem);
            return newItem;
        }
    }
}
