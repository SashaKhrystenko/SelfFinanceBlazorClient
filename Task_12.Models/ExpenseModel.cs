using System;

namespace Task_12.Models
{
    public class ExpenseModel
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
    }
}
