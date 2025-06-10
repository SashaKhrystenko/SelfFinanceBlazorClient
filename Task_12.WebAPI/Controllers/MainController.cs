using Microsoft.AspNetCore.Mvc;
using System;
using Task_12.Services.Interfaces;
using Task_12.Structures;

namespace Task_12.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IExpenseService _expenseService;

        public MainController(IIncomeService incomeService, IExpenseService expenseService)
        {
            if (incomeService == null)
            {
                throw new ArgumentNullException(nameof(incomeService), $"{nameof(incomeService)} is null.");
            }

            if (expenseService == null)
            {
                throw new ArgumentNullException(nameof(expenseService), $"{nameof(expenseService)} is null.");
            }

            _incomeService = incomeService;
            _expenseService = expenseService;
        }

        /// <summary>
        /// Retrieves a daily financial report for the specified date.
        /// </summary>
        /// <param name="date">The date for which to generate the report.</param>
        /// <returns>A financial report containing incomes and expenses for the specified date.</returns>
        /// <response code="200">The daily financial report was successfully generated.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("get-daily-report")]
        public IActionResult GetDailyReport([FromQuery] DateOnly date)
        {
            FinancialReportResult reportResult = new(
                incomes: _incomeService.GetIncomesByDate(date),
                expenses: _expenseService.GetExpensesByDate(date)
            );

            return Ok(reportResult);
        }

        /// <summary>
        /// Retrieves a financial report for the specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the report period.</param>
        /// <param name="endDate">The end date of the report period.</param>
        /// <returns>A financial report containing incomes and expenses for the specified period.</returns>
        /// <response code="200">The financial report for the date range was successfully generated.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("get-date-period-report")]
        public IActionResult GetDatePeriodReport([FromQuery] DateOnly startDate, DateOnly endDate)
        {
            FinancialReportResult reportResult = new(
                incomes: _incomeService.GetIncomesByDateRange(startDate, endDate),
                expenses: _expenseService.GetExpensesByDateRange(startDate, endDate)
            );

            return Ok(reportResult);
        }
    }
}
