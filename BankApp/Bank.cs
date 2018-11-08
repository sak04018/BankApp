using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BankApp
{
    static class Bank
    {
        private static BankModel db = new BankModel();
        /// <summary>
        /// Create an account in the bank
        /// </summary>
        /// <param name="emailAddress">Email address associated with the account</param>
        /// <param name="accountType">Type of account</param>
        /// <param name="initialAmount">Initial amount to deposit</param>
        /// <returns>New account created</returns>
        /// <exception cref="ArgumentNullException" />
        public static Account CreateAccount(string emailAddress, TypeOfAccount accountType = TypeOfAccount.Checking, decimal initialAmount = 0)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new ArgumentNullException(nameof(emailAddress), "Email Address is required!");
            }
            var account = new Account
            {
                EmailAddress = emailAddress,
                AccountType = accountType,

            };

            if (initialAmount > 0)
            {
                account.Deposit(initialAmount);
            }
            db.Accounts.Add(account);
            db.SaveChanges();
            return account;
        }

        public static IEnumerable<Account> GetAllAccounts(string emailAddress)
        {
            return db.Accounts.Where(a => a.EmailAddress == emailAddress);
        }

        public static void Deposit(int accountNumber, decimal amount)
        {
            var account = db.Accounts.SingleOrDefault(a => a.AccountNumber == accountNumber);
            if (account == null)
            {
                throw new ArgumentException("Invalid account number!",nameof(accountNumber));
            }
            account.Deposit(amount);

            var transaction = new Transaction
            {
                Description = "Bank Deposit",
                TransactionDate = DateTime.Now,
                TypeofTransaction = TransactionType.Credit,
                Amount = amount,
                AccountNumber = accountNumber
            };
            db.Transactions.Add(transaction);
            db.SaveChanges();
        }

        public static void Withdraw(int accountNumber, decimal amount)
        {
            var account = db.Accounts.SingleOrDefault(a => a.AccountNumber == accountNumber);
            if (account == null)
            {
                throw new ArgumentException("Invalid account number!", nameof(accountNumber));
            }
            account.Withdraw(amount);

            var transaction = new Transaction
            {
                Description = "Bank Withdrawal",
                TransactionDate = DateTime.Now,
                TypeofTransaction = TransactionType.Debit,
                Amount = amount,
                AccountNumber = accountNumber
            };
            db.Transactions.Add(transaction);
            db.SaveChanges();
        }

        public static IEnumerable<Transaction> GetAllTransactions(int accountNumber)
        {
           return db.Transactions.Where(t => t.AccountNumber == accountNumber).OrderByDescending(t => t.TransactionDate);
        }
    }
}
