using System;
using System.Collections.Generic;
using Task_12.Entities;

namespace Task_12.Repositories.Interfaces
{
    public interface IIncomeRepository
    {
        public IReadOnlyList<IncomeEntity> GetAll(bool includeAdditionalData);
        public IReadOnlyList<IncomeEntity> Find(Func<IncomeEntity, bool> predicate, bool includeAdditionalData);
        public IncomeEntity FirstOrDefault(Func<IncomeEntity, bool> predicate, bool includeAdditionalData);
        public void Add(IncomeEntity income);
        public void Update(IncomeEntity income);
        public void Delete(IncomeEntity income);
    }
}
