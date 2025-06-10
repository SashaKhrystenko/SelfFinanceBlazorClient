using System;
using System.Collections.Generic;
using System.Linq;
using Task_12.Models;

namespace Task_12.Structures
{
    public struct FinancialReportResult
    {
        public IReadOnlyList<IncomeModel> Incomes { get; private set; }
        public IReadOnlyList<ExpenseModel> Expenses { get; private set; }

        public decimal TotalIncomeAmount { get; private set; }
        public decimal TotalExpenseAmount { get; private set; }

        public FinancialReportResult(IReadOnlyList<IncomeModel> incomes, IReadOnlyList<ExpenseModel> expenses)
        {
            if (incomes == null)
            {
                throw new ArgumentNullException(nameof(incomes), $"{nameof(incomes)} is null.");
            }

            if (expenses == null)
            {
                throw new ArgumentNullException(nameof(expenses), $"{nameof(expenses)} is null.");
            }

            Incomes = incomes;
            Expenses = expenses;

            TotalIncomeAmount = incomes.Sum(income => income.Amount);
            TotalExpenseAmount = expenses.Sum(expense => expense.Amount);
        }
    }
}
