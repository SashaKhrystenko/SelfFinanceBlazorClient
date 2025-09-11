using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;

namespace Task_12.ApiRoutes
{
    public static class SelfFinanceApiRoutes
    {
        public static class ExpenseController
        {
            private const string _baseControllerRoute = "/expenses";

            public const string AddExpense = $"{_baseControllerRoute}/add-expense";
            public const string UpdateExpense = $"{_baseControllerRoute}/update-expense";

            public static string MakeGetSortedExpensesRoute(ExpenseFilter expenseFilter, bool byDescending)
            {
                Dictionary<string, string> queryParams = new()
                {
                    { nameof(expenseFilter), expenseFilter.ToString() },
                    { nameof(byDescending), byDescending.ToString() }
                };

                return QueryHelpers.AddQueryString($"{_baseControllerRoute}/get-sorted-expenses", queryParams);
            }

            public static string MakeDeleteExpenseRoute(Guid id)
            {
                return $"{_baseControllerRoute}/delete-expense/{id}";
            }

            public static string MakeGetExpenseByIdRoute(Guid id)
            {
                return $"{_baseControllerRoute}/get-expense-by-id?id={id}";
            }
        }

        public static class ExpenseCategoryController
        {
            private const string _baseControllerRoute = "/expense-categories";

            public const string GetAllExpenseCategories = $"{_baseControllerRoute}/get-all-expense-categories";
            public const string AddExpenseCategory = $"{_baseControllerRoute}/add-expense-category";
            public const string UpdateExpenseCategory = $"{_baseControllerRoute}/update-expense-category";

            public static string MakeGetSortedExpenseCategoriesRoute(ExpenseCategoryFilter expenseCategoryFilter, bool byDescending)
            {
                Dictionary<string, string> queryParams = new()
                {
                    { nameof(expenseCategoryFilter), expenseCategoryFilter.ToString() },
                    { nameof(byDescending), byDescending.ToString() }
                };

                return QueryHelpers.AddQueryString($"{_baseControllerRoute}/get-sorted-expense-categories", queryParams);
            }

            public static string MakeGetExpenseCategoryByIdRoute(Guid id)
            {
                return $"{_baseControllerRoute}/get-expense-by-id?id={id}";
            }

            public static string MakeDeleteExpenseCategorydRoute(Guid id)
            {
                return $"{_baseControllerRoute}/delete-expense-category/{id}";
            }
        }

        public static class IncomeController
        {
            private const string _baseControllerRoute = "/incomes";

            public const string AddIncome = $"{_baseControllerRoute}/add-income";
            public const string UpdateIncome = $"{_baseControllerRoute}/update-income";

            public static string MakeGetSortedIncomeRoute(IncomeFilter incomeFilter, bool byDescending)
            {
                Dictionary<string, string> queryParams = new()
                {
                    { nameof(incomeFilter), incomeFilter.ToString() },
                    { nameof(byDescending), byDescending.ToString() }
                };

                return QueryHelpers.AddQueryString($"{_baseControllerRoute}/get-sorted-incomes", queryParams);
            }

            public static string MakeDeleteIncomeRoute(Guid id)
            {
                return $"{_baseControllerRoute}/delete-income/{id}";
            }

            public static string MakeGetIncomeByIdRoute(Guid id)
            {
                return $"{_baseControllerRoute}/get-income-by-id?id={id}";
            }
        }

        public static class IncomeCategoryController
        {
            private const string _baseControllerRoute = "/income-categories";

            public const string GetAllIncomeCategories = $"{_baseControllerRoute}/get-all-income-categories";
            public const string AddIncomeCategory = $"{_baseControllerRoute}/add-income-category";
            public const string UpdateIncomeCategory = $"{_baseControllerRoute}/update-income-category";

            public static string MakeGetSortedIncomeCategoriesRoute(IncomeCategoryFilter incomeCategoryFilter, bool byDescending)
            {
                Dictionary<string, string> queryParams = new()
                {
                    { nameof(incomeCategoryFilter), incomeCategoryFilter.ToString() },
                    { nameof(byDescending), byDescending.ToString() }
                };

                return QueryHelpers.AddQueryString($"{_baseControllerRoute}/get-sorted-income-categories", queryParams);
            }

            public static string MakeGetIncomeCategoryByIdRoute(Guid id)
            {
                return $"{_baseControllerRoute}/get-income-by-id?id={id}";
            }

            public static string MakeDeleteIncomeCategorydRoute(Guid id)
            {
                return $"{_baseControllerRoute}/delete-income-category/{id}";
            }
        }

        public static class FinancialSummaryController
        {
            private const string _baseControllerRoute = "/financial-summary";

            public const string GetBalance = $"{_baseControllerRoute}/get-balance";
            public const string GetAllTransactions = $"{_baseControllerRoute}/get-all-transactions";

            public static string MakeGetDailyReportRoute(DateOnly date)
            {
                Dictionary<string, string> queryParams = new()
                {
                    { nameof(date), date.ToString() }
                };

                return QueryHelpers.AddQueryString($"{_baseControllerRoute}/get-daily-report", queryParams);
            }

            public static string MakeGetDatePeriodReportRoute(DateOnly startDate, DateOnly endDate)
            {
                Dictionary<string, string> queryParams = new()
                {
                    { nameof(startDate), startDate.ToString() },
                    { nameof(endDate), endDate.ToString() }
                };

                return QueryHelpers.AddQueryString($"{_baseControllerRoute}/get-date-period-report", queryParams);
            }
        }
    }
}
