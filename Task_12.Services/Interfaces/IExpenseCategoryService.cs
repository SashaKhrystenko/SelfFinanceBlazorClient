using System.Collections.Generic;
using Task_12.Models;

namespace Task_12.Services.Interfaces
{
    public interface IExpenseCategoryService
    {
        public IReadOnlyList<ExpenseCategoryModel> GetAll();
        public bool Exist(string categoryName);
        public void Add(ExpenseCategoryModel expenseCategoryModel);
        public void Update(ExpenseCategoryModel expenseCategoryModel);
        public void Delete(ExpenseCategoryModel expenseCategoryModel);
    }
}
