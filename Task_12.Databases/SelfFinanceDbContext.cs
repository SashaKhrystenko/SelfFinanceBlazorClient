using Microsoft.EntityFrameworkCore;
using System;
using Task_12.Databases.EntityConfiguretions;
using Task_12.Databases.Interfaces;
using Task_12.Entities;

namespace Task_12.Databases
{
    public class SelfFinanceDbContext : DbContext, ISelfFinanceDbContext
    {
        public SelfFinanceDbContext(DbContextOptions<SelfFinanceDbContext> options)
            : base(options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), $"{options} is null.");
            }
        }

        public DbSet<IncomeCategoryEntity> IncomeCategories { get; set; }
        public DbSet<IncomeEntity> Incomes { get; set; }
        public DbSet<ExpenseCategoryEntity> ExpenseCategories { get; set; }
        public DbSet<ExpenseEntity> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new IncomeCategoryEntityConfiguretion());
            modelBuilder.ApplyConfiguration(new IncomeEntityConfiguretion());
            modelBuilder.ApplyConfiguration(new ExpenseCategoryEntityConfiguretion());
            modelBuilder.ApplyConfiguration(new ExpenseEntityConfiguretion());
        }
    }
}
