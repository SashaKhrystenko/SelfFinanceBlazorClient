using System;
using System.Collections.Generic;

namespace Task_12.Entities
{
    public class ExpenseCategoryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<ExpenseEntity> Expenses { get; set; }
    }
}
