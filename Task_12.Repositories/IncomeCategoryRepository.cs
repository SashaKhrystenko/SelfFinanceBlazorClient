using System;
using System.Collections.Generic;
using System.Linq;
using Task_12.Databases.Interfaces;
using Task_12.Entities;
using Task_12.Repositories.Interfaces;

namespace Task_12.Repositories
{
    public class IncomeCategoryRepository : IIncomeCategoryRepository
    {
        private readonly ISelfFinanceDbContext _selfFinanceDbContext;

        public IncomeCategoryRepository(ISelfFinanceDbContext selfFinanceDbContext)
        {
            if (selfFinanceDbContext == null)
            {
                throw new ArgumentNullException(nameof(selfFinanceDbContext), $"{nameof(selfFinanceDbContext)} is null.");
            }

            _selfFinanceDbContext = selfFinanceDbContext;
        }

        public IReadOnlyList<IncomeCategoryEntity> GetAll()
        {
            return _selfFinanceDbContext.IncomeCategories.ToArray();
        }

        public IncomeCategoryEntity FirstOrDefault(Func<IncomeCategoryEntity, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), $"{nameof(predicate)} is null.");
            }

            return _selfFinanceDbContext.IncomeCategories.FirstOrDefault(predicate);
        }

        public void Add(IncomeCategoryEntity incomeCategory)
        {
            if (incomeCategory == null)
            {
                throw new ArgumentNullException(nameof(incomeCategory), $"{nameof(incomeCategory)} is null.");
            }

            _selfFinanceDbContext.IncomeCategories.Add(incomeCategory);
            _selfFinanceDbContext.SaveChanges();
        }

        public void Delete(IncomeCategoryEntity incomeCategory)
        {
            if (incomeCategory == null)
            {
                throw new ArgumentNullException(nameof(incomeCategory), $"{nameof(incomeCategory)} is null.");
            }

            _selfFinanceDbContext.IncomeCategories.Remove(incomeCategory);
            _selfFinanceDbContext.SaveChanges();
        }

        public void Update(IncomeCategoryEntity incomeCategory)
        {
            if (incomeCategory == null)
            {
                throw new ArgumentNullException(nameof(incomeCategory), $"{nameof(incomeCategory)} is null.");
            }

            _selfFinanceDbContext.IncomeCategories.Update(incomeCategory);
            _selfFinanceDbContext.SaveChanges();
        }
    }
}
