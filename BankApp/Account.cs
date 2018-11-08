using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BankApp
{
    public enum TypeOfAccount
    {
        Savings,
        Checking,
        CD,
        Loan
    }
    /// <summary>
    /// Defines an account for a bank
    /// </summary>
    public class Account
    {

        #region Properties
        /// <summary>
        /// Account number of the account
        /// </summary>
        public int AccountNumber { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public decimal Balance { get; private set; }
        public TypeOfAccount AccountType { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        #endregion

        #region Constructors

        public Account()
        {
            CreatedDate = DateTime.Now;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deposit money into my account
        /// </summary>
        /// <param name="amount">Amount to deposit</param>
        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                throw new NSFException("Insufficient funds!");
            }
            Balance -= amount;
        }
        #endregion
    }
}
