using System;

namespace Task_12.Entities
{
    public class IncomeEntity
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public Guid IncomeCategoryId { get; set; }
        public decimal Amount { get; set; }

        public IncomeCategoryEntity IncomeCategory { get; set; }
    }
}
