using System;
using System.Collections.Generic;
using Task_12.Models;

namespace Task_12.Services.Interfaces
{
    public interface IExpenseService
    {
        public IReadOnlyList<ExpenseModel> GetAll();
        public IReadOnlyList<ExpenseModel> GetExpensesByDate(DateOnly date);
        public IReadOnlyList<ExpenseModel> GetExpensesByDateRange(DateOnly startDate, DateOnly endDate);
        public void Add(ExpenseModel expenseModel);
        public void Update(ExpenseModel expenseModel);
        public void Delete(ExpenseModel expenseModel);
    }
}
