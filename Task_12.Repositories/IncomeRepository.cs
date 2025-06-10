using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Task_12.Databases.Interfaces;
using Task_12.Entities;
using Task_12.Repositories.Interfaces;

namespace Task_12.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly ISelfFinanceDbContext _selfFinanceDbContext;

        public IncomeRepository(ISelfFinanceDbContext selfFinanceDbContext)
        {
            if (selfFinanceDbContext == null)
            {
                throw new ArgumentNullException(nameof(selfFinanceDbContext), $"{nameof(selfFinanceDbContext)} is null.");
            }

            _selfFinanceDbContext = selfFinanceDbContext;
        }

        public IReadOnlyList<IncomeEntity> Find(Func<IncomeEntity, bool> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException(nameof(func), $"{nameof(func)} is null.");
            }

            return _selfFinanceDbContext.Incomes.Where(func).ToArray();
        }

        public IReadOnlyList<IncomeEntity> GetAll(bool includeAdditionalData)
        {
            if (includeAdditionalData)
            {
                return _selfFinanceDbContext.Incomes
                    .Include(income => income.IncomeCategory)
                    .ToArray()
                ;
            }

            return _selfFinanceDbContext.Incomes.ToArray();
        }

        public IReadOnlyList<IncomeEntity> Find(Func<IncomeEntity, bool> predicate, bool includeAdditionalData)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), $"{nameof(predicate)} is null.");
            }

            if (includeAdditionalData)
            {
                return _selfFinanceDbContext.Incomes
                    .Include(income => income.IncomeCategory)
                    .Where(predicate)
                    .ToArray()
                ;
            }

            return _selfFinanceDbContext.Incomes.Where(predicate).ToArray();
        }

        public IncomeEntity FirstOrDefault(Func<IncomeEntity, bool> predicate, bool includeAdditionalData)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), $"{nameof(predicate)} is null.");
            }

            if (includeAdditionalData)
            {
                return _selfFinanceDbContext.Incomes
                    .Include(expense => expense.IncomeCategory)
                    .FirstOrDefault(predicate)
                ;
            }

            return _selfFinanceDbContext.Incomes.FirstOrDefault(predicate);
        }

        public void Add(IncomeEntity income)
        {
            if (income == null)
            {
                throw new ArgumentNullException(nameof(income), $"{nameof(income)} is null.");
            }

            _selfFinanceDbContext.Incomes.Add(income);
            _selfFinanceDbContext.SaveChanges();
        }

        public void Delete(IncomeEntity income)
        {
            if (income == null)
            {
                throw new ArgumentNullException(nameof(income), $"{nameof(income)} is null.");
            }

            _selfFinanceDbContext.Incomes.Remove(income);
            _selfFinanceDbContext.SaveChanges();
        }

        public void Update(IncomeEntity income)
        {
            if (income == null)
            {
                throw new ArgumentNullException(nameof(income), $"{nameof(income)} is null.");
            }

            _selfFinanceDbContext.Incomes.Update(income);
            _selfFinanceDbContext.SaveChanges();
        }
    }
}
