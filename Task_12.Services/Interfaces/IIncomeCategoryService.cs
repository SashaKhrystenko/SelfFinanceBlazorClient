using System.Collections.Generic;
using Task_12.Models;

namespace Task_12.Services.Interfaces
{
    public interface IIncomeCategoryService
    {
        public IReadOnlyList<IncomeCategoryModel> GetAll();
        public bool Exist(string categoryName);
        public void Add(IncomeCategoryModel incomeCategoryModel);
        public void Update(IncomeCategoryModel incomeCategoryModel);
        public void Delete(IncomeCategoryModel incomeCategoryModel);
    }
}
