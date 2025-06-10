using System;
using System.Collections.Generic;
using Task_12.Entities;

namespace Task_12.Repositories.Interfaces
{
    public interface IIncomeCategoryRepository
    {
        public IReadOnlyList<IncomeCategoryEntity> GetAll();
        public IncomeCategoryEntity FirstOrDefault(Func<IncomeCategoryEntity, bool> predicate);
        public void Add(IncomeCategoryEntity incomeCategory);
        public void Update(IncomeCategoryEntity incomeCategory);
        public void Delete(IncomeCategoryEntity incomeCategory);
    }
}
