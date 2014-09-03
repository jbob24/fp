using FinanceProjector.Enums;
using FinanceProjector.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FinanceProjector.Repository;
using MongoDB.Bson;

namespace FinanceProjector.Model
{
    public class Transaction // : IMongoEntity
    {
        //public ObjectId Id { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string TransactionID { get; set; }
        public string Name { get; set; }
        public string Comments { get; set; }
        public string CheckNum { get; set; }
        public string ReferenceNumber { get; set; }
        public string PayeeID { get; set; }
        public string Currency { get; set; }
        public bool IsEstimate { get; set; }
        public string BudgetCategoryName { get; set; }

        public void UpdateData(Transaction source)
        {
            this.TransactionType = source.TransactionType;
            this.Date = source.Date;
            this.Amount = source.Amount;
            this.Name = source.Name;
            this.Comments = source.Comments;
            this.CheckNum = source.CheckNum;
            this.ReferenceNumber = source.ReferenceNumber;
            this.PayeeID = source.PayeeID;
            this.Currency = source.Currency;
            this.IsEstimate = source.IsEstimate;
            this.BudgetCategoryName = source.BudgetCategoryName;
        }

        //public void Save()
        //{
        //   var repo = new MongoRepository<Transaction>();
        //    repo.Save(this);
        //}

        //public static List<Transaction> SaveNew(List<Transaction> transactions)
        //{
        //    var repo = new MongoRepository<Transaction>();
        //    var addedTransactions = new List<Transaction>();

        //    foreach (var item in transactions)
        //    {
        //        if (!repo.Exists(t => t.TransactionID == item.TransactionID))
        //        {
        //            addedTransactions.Add(item);
        //        }
        //    }

        //    if (addedTransactions.Any())
        //    {
        //        repo.InsertBatch(addedTransactions);
        //    }

        //    return addedTransactions;
        //}
    } 
}
