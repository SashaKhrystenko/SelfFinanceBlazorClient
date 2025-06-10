using System;
using System.Collections.Generic;
using Task_12.Models;

namespace Task_12.Services.Interfaces
{
    public interface IIncomeService
    {
        public IReadOnlyList<IncomeModel> GetAll();
        public IReadOnlyList<IncomeModel> GetIncomesByDate(DateOnly date);
        public IReadOnlyList<IncomeModel> GetIncomesByDateRange(DateOnly startDate, DateOnly endDate);
        public void Add(IncomeModel incomeModel);
        public void Update(IncomeModel incomeModel);
        public void Delete(IncomeModel incomeModel);
    }
}
