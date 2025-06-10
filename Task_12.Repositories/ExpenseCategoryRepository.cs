using System;
using System.Collections.Generic;
using System.Linq;
using Task_12.Databases.Interfaces;
using Task_12.Entities;
using Task_12.Repositories.Interfaces;

namespace Task_12.Repositories
{
    public class ExpenseCategoryRepository : IExpenseCategoryRepository
    {
        private readonly ISelfFinanceDbContext _selfFinanceDbContext;

        public ExpenseCategoryRepository(ISelfFinanceDbContext selfFinanceDbContext)
        {
            if (selfFinanceDbContext == null)
            {
                throw new ArgumentNullException(nameof(selfFinanceDbContext), $"{nameof(selfFinanceDbContext)} is null.");
            }

            _selfFinanceDbContext = selfFinanceDbContext;
        }

        public IReadOnlyList<ExpenseCategoryEntity> GetAll()
        {
            return _selfFinanceDbContext.ExpenseCategories.ToArray();
        }

        public ExpenseCategoryEntity FirstOrDefault(Func<ExpenseCategoryEntity, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), $"{nameof(predicate)} is null.");
            }

            return _selfFinanceDbContext.ExpenseCategories.FirstOrDefault(predicate);
        }

        public void Add(ExpenseCategoryEntity expenseCategory)
        {
            if (expenseCategory == null)
            {
                throw new ArgumentNullException(nameof(expenseCategory), $"{nameof(expenseCategory)} is null.");
            }

            _selfFinanceDbContext.ExpenseCategories.Add(expenseCategory);
            _selfFinanceDbContext.SaveChanges();
        }

        public void Delete(ExpenseCategoryEntity expenseCategory)
        {
            if (expenseCategory == null)
            {
                throw new ArgumentNullException(nameof(expenseCategory), $"{nameof(expenseCategory)} is null.");
            }

            _selfFinanceDbContext.ExpenseCategories.Remove(expenseCategory);
            _selfFinanceDbContext.SaveChanges();
        }

        public void Update(ExpenseCategoryEntity expenseCategory)
        {
            if (expenseCategory == null)
            {
                throw new ArgumentNullException(nameof(expenseCategory), $"{nameof(expenseCategory)} is null.");
            }

            _selfFinanceDbContext.ExpenseCategories.Update(expenseCategory);
            _selfFinanceDbContext.SaveChanges();
        }
    }
}
