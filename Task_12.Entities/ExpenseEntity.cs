using System;

namespace Task_12.Entities
{
    public class ExpenseEntity
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public Guid ExpenseCategoryId { get; set; }
        public decimal Amount { get; set; }

        public ExpenseCategoryEntity ExpenseCategory { get; set; }
    }
}
