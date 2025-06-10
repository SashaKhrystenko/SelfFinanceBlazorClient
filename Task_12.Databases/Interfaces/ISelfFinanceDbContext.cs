using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Task_12.Entities;

namespace Task_12.Databases.Interfaces
{
    public interface ISelfFinanceDbContext
    {
        public DbSet<IncomeCategoryEntity> IncomeCategories { get; set; }
        public DbSet<IncomeEntity> Incomes { get; set; }
        public DbSet<ExpenseCategoryEntity> ExpenseCategories { get; set; }
        public DbSet<ExpenseEntity> Expenses { get; set; }
        public DatabaseFacade Database { get; }
        public int SaveChanges();
    }
}
