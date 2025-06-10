using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Task_12.Databases.Interfaces;
using Task_12.Entities;
using Task_12.Repositories.Interfaces;

namespace Task_12.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ISelfFinanceDbContext _selfFinanceDbContext;

        public ExpenseRepository(ISelfFinanceDbContext selfFinanceDbContext)
        {
            if (selfFinanceDbContext == null)
            {
                throw new ArgumentNullException(nameof(selfFinanceDbContext), $"{nameof(selfFinanceDbContext)} is null.");
            }

            _selfFinanceDbContext = selfFinanceDbContext;
        }

        public IReadOnlyList<ExpenseEntity> GetAll(bool includeAdditionalData)
        {
            if (includeAdditionalData)
            {
                return _selfFinanceDbContext.Expenses
                    .Include(expense => expense.ExpenseCategory)
                    .ToArray()
                ;
            }

            return _selfFinanceDbContext.Expenses.ToArray();
        }

        public IReadOnlyList<ExpenseEntity> Find(Func<ExpenseEntity, bool> predicate, bool includeAdditionalData)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), $"{nameof(predicate)} is null.");
            }

            if (includeAdditionalData)
            {
                return _selfFinanceDbContext.Expenses
                    .Include(expense => expense.ExpenseCategory)
                    .Where(predicate)
                    .ToArray()
                ;
            }

            return _selfFinanceDbContext.Expenses.Where(predicate).ToArray();
        }

        public ExpenseEntity FirstOrDefault(Func<ExpenseEntity, bool> predicate, bool includeAdditionalData)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), $"{nameof(predicate)} is null.");
            }

            if (includeAdditionalData)
            {
                return _selfFinanceDbContext.Expenses
                    .Include(expense => expense.ExpenseCategory)
                    .FirstOrDefault(predicate)
                ;
            }

            return _selfFinanceDbContext.Expenses.FirstOrDefault(predicate);
        }

        public void Add(ExpenseEntity expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense), $"{nameof(expense)} is null.");
            }

            _selfFinanceDbContext.Expenses.Add(expense);
            _selfFinanceDbContext.SaveChanges();
        }

        public void Delete(ExpenseEntity expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense), $"{nameof(expense)} is null.");
            }

            _selfFinanceDbContext.Expenses.Remove(expense);
            _selfFinanceDbContext.SaveChanges();
        }

        public void Update(ExpenseEntity expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense), $"{nameof(expense)} is null.");
            }

            _selfFinanceDbContext.Expenses.Update(expense);
            _selfFinanceDbContext.SaveChanges();
        }
    }
}
