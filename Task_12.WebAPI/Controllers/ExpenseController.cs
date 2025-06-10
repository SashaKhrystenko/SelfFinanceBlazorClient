using Microsoft.AspNetCore.Mvc;
using System;
using Task_12.Models;
using Task_12.Services.Interfaces;

namespace Task_12.Controllers
{
    [Route("expenses")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IExpenseCategoryService _expenseCategoryService;

        public ExpenseController(IExpenseService expenseService, IExpenseCategoryService expenseCategoryService)
        {
            if (expenseService == null)
            {
                throw new ArgumentNullException(nameof(expenseService), $"{nameof(expenseService)} is null.");
            }

            if (expenseCategoryService == null)
            {
                throw new ArgumentNullException(nameof(expenseCategoryService), $"{nameof(expenseCategoryService)} is null.");
            }

            _expenseService = expenseService;
            _expenseCategoryService = expenseCategoryService;
        }

        /// <summary>
        /// Retrieves all expenses.
        /// </summary>
        /// <returns>A list of expense records.</returns>
        /// <response code="200">Returns the list of expenses.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("get-all-expenses")]
        public IActionResult GetAllExpenses()
        {
            return Ok(_expenseService.GetAll());
        }

        /// <summary>
        /// Adds a new expense.
        /// </summary>
        /// <param name="category">The expense category.</param>
        /// <param name="amount">The amount of spent monney.</param>
        /// <returns>A success message if the expense was added.</returns>
        /// <response code="200">The expense was successfully added.</response>
        /// <response code="400">Entered invalid data.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("add-expense")]
        public IActionResult AddExpense([FromForm] string category, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest($"{nameof(category)} is null or white space.");
            }

            if (!_expenseCategoryService.Exist(category))
            {
                return BadRequest($"Category {category} doesn't exist.");
            }

            if (amount <= 0)
            {
                return BadRequest($"{nameof(amount)} can't be less than zero.");
            }

            ExpenseModel model = new()
            {
                Id = Guid.NewGuid(),
                Date = DateOnly.FromDateTime(DateTime.Now),
                Amount = amount,
                Category = category
            };

            _expenseService.Add(model);

            return Ok("Expense was added.");
        }

        /// <summary>
        /// Updates an existing expense.
        /// </summary>
        /// <param name="model">The updated expense model.</param>
        /// <returns>A success message if the expense was updated.</returns>
        /// <response code="200">The expense was successfully updated.</response>
        /// <response code="400">The model is null or invalid.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch("update-expense")]
        public IActionResult UpdateExpense([FromBody] ExpenseModel model)
        {
            if (model == null)
            {
                return BadRequest($"{nameof(model)} is null.");
            }

            if (string.IsNullOrWhiteSpace(model.Category))
            {
                return BadRequest($"{nameof(model.Category)} is null or white space.");
            }

            if (!_expenseCategoryService.Exist(model.Category))
            {
                return BadRequest($"Category {model.Category} doesn't exist.");
            }

            if (model.Amount <= 0)
            {
                return BadRequest($"{nameof(model.Amount)} can't be less than zero.");
            }

            _expenseService.Update(model);

            return Ok("Expense was updated.");
        }

        /// <summary>
        /// Deletes an expense.
        /// </summary>
        /// <param name="model">The expense model to delete.</param>
        /// <returns>A success message if the expense was deleted.</returns>
        /// <response code="200">The expense was successfully deleted.</response>
        /// <response code="400">The model is null.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("delete-expense")]
        public IActionResult DeleteExpense([FromBody] ExpenseModel model)
        {
            if (model == null)
            {
                return BadRequest($"{nameof(model)} is null.");
            }

            _expenseService.Delete(model);

            return Ok("Expense was deleted.");
        }
    }
}
