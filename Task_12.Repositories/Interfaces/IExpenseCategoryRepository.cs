using System;
using System.Collections.Generic;
using Task_12.Entities;

namespace Task_12.Repositories.Interfaces
{
    public interface IExpenseCategoryRepository
    {
        public IReadOnlyList<ExpenseCategoryEntity> GetAll();
        public ExpenseCategoryEntity FirstOrDefault(Func<ExpenseCategoryEntity, bool> predicate);
        public void Add(ExpenseCategoryEntity expenseCategory);
        public void Update(ExpenseCategoryEntity expenseCategory);
        public void Delete(ExpenseCategoryEntity expenseCategory);
    }
}
