using System;
using System.Collections.Generic;

namespace Task_12.Entities
{
    public class IncomeCategoryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<IncomeEntity> Incomes { get; set; }
    }
}
