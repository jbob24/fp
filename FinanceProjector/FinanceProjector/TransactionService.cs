﻿using FinanceProjector.Enums;
using FinanceProjector.Interfaces;
using FinanceProjector.Model;
using FinanceProjector.TransactionProviders.OFX;
using FinanceProjector.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceProjector
{
    public class TransactionService
    {
        private MongoRepository<User> _repo;
        private User _user;

        public TransactionService()
        {
            _repo = new MongoRepository<User>();
        }

        public void DeleteUser(User user)
        {
            ValidateUser(user);

            _repo.Delete(user);
        }

        public void AddUser(User user)
        {
            ValidateUser(user);

            _repo.Save(user);
        }

        public User GetUserByUserId(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException("userId");
            }

            return _repo.FirstOrDefault(u => u.UserId == userId);
        }

        public void SaveUser(User user)
        {
            ValidateUser(user);

            _repo.Save(user);
        }

        public List<Transaction> ImportTransactions(User user, string filePath, TransactionProviderType providerType)
        {
            ValidateUser(user);

            _user = user;

            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }
                
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(string.Format("The file at {0} was not found.", filePath));
            }

            List<Transaction> transactions = null;
            List<Transaction> newTransactions = null;
            
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                switch(providerType)
                {
                    case TransactionProviderType.OFXTransactionProvider:
                        transactions = new OFXTransactionProvider().LoadTransactions(fileStream);
                        break;
                }
            }

            var insertedTransactions = InsertNewTransactions(transactions);

            _repo.Save(_user);
            return insertedTransactions;
        }

        private List<Transaction> InsertNewTransactions(List<Transaction> transactions)
        {
            var addedTransactions = new List<Transaction>();

            foreach (var item in transactions)
            {
                AssignBudgetCategory(item);

                var existingTransaction = _user.Transactions.FirstOrDefault(t => t.TransactionID == item.TransactionID);

                if (existingTransaction != null)
                {
                    existingTransaction.UpdateData(item);
                }
                else
                {
                    addedTransactions.Add(item);
                }
            }

            if (addedTransactions.Any())
            {
                _user.Transactions.AddRange(addedTransactions);
            }

            return addedTransactions;
        }

        private void ValidateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
        }

        private void AssignBudgetCategory(Transaction transaction)
        {
            foreach (var budgetCategory in _user.BudgetCategories)
            {
                FindMatchingBudgetCategory(transaction, budgetCategory);
            }
        }

        private void FindMatchingBudgetCategory(Transaction transaction, BudgetCategory budgetCategory)
        {
            if (!FindBudgetCategorName(transaction, budgetCategory))
            {
                foreach (var subCategory in budgetCategory.SubCategories)
                {
                    FindMatchingBudgetCategory(transaction, subCategory);
                }
            }
        }

        private static bool FindBudgetCategorName(Transaction transaction, BudgetCategory budgetCategory)
        {
            try
            {
                foreach (var transactionMatch in budgetCategory.TransactionMatches)
                {
                    if (transactionMatch.IsMatch(transaction))
                    {
                        transaction.BudgetCategoryName = budgetCategory.Name;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            return false;
        }
    }
}
