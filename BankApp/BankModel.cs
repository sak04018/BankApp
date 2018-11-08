using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BankApp
{
    public class BankModel : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BankC2Db;Integrated Security=True;Connect Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountNumber)
                .HasName("PK_Accounts");

                entity.Property(e => e.AccountNumber)
                .ValueGeneratedOnAdd();

                entity.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(50);

                entity.ToTable("Accounts");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                .HasName("PK_Transactions");

                entity.Property(e => e.TransactionId)
                .ValueGeneratedOnAdd();

                entity.ToTable("Transactions");
            });
        }
    }
}
