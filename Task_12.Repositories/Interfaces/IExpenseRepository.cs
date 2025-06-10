using System;
using System.Collections.Generic;
using Task_12.Entities;

namespace Task_12.Repositories.Interfaces
{
    public interface IExpenseRepository
    {
        public IReadOnlyList<ExpenseEntity> GetAll(bool includeAdditionalData);
        public IReadOnlyList<ExpenseEntity> Find(Func<ExpenseEntity, bool> predicate, bool includeAdditionalData);
        public ExpenseEntity FirstOrDefault(Func<ExpenseEntity, bool> predicate, bool includeAdditionalData);
        public void Add(ExpenseEntity expense);
        public void Update(ExpenseEntity expense);
        public void Delete(ExpenseEntity expense);
    }
}
