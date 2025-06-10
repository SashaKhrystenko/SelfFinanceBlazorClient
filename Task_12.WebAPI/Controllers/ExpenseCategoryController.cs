using Microsoft.AspNetCore.Mvc;
using System;
using Task_12.Models;
using Task_12.Services.Interfaces;

namespace Task_12.Controllers
{
    [Route("expense-categories")]
    public class ExpenseCategoryController : ControllerBase
    {
        private readonly IExpenseCategoryService _expenseCategoryService;

        public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService)
        {
            if (expenseCategoryService == null)
            {
                throw new ArgumentNullException(nameof(expenseCategoryService), $"{nameof(expenseCategoryService)} is null.");
            }

            _expenseCategoryService = expenseCategoryService;
        }

        /// <summary>
        /// Retrieves all expense categories.
        /// </summary>
        /// <returns>A list of expense categories.</returns>
        /// <response code="200">Returns the list of expense categories.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("get-all-expense-categories")]
        public IActionResult GetAllExpenseCategories()
        {
            return Ok(_expenseCategoryService.GetAll());
        }

        /// <summary>
        /// Adds a new expense category.
        /// </summary>
        /// <param name="name">The expense category name.</param>
        /// <returns>A success message if the category was added.</returns>
        /// <response code="200">The expense category was successfully added.</response>
        /// <response code="400">The name is null or white space.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("add-expense-category")]
        public IActionResult AddExpenseCategory([FromForm] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest($"{nameof(name)} is null or white space.");
            }

            if (_expenseCategoryService.Exist(name))
            {
                return BadRequest($"Category {name} is already exists.");
            }

            ExpenseCategoryModel model = new()
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            _expenseCategoryService.Add(model);

            return Ok("Expense category was added");
        }

        /// <summary>
        /// Updates an existing expense category.
        /// </summary>
        /// <param name="model">The updated expense category model.</param>
        /// <returns>A success message if the category was updated.</returns>
        /// <response code="200">The expense category was successfully updated.</response>
        /// <response code="400">Enteread invalid data.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch("update-expense-category")]
        public IActionResult UpdateExpenseCategory([FromBody] ExpenseCategoryModel model)
        {
            if (model == null)
            {
                return BadRequest($"{nameof(model)} is null.");
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest($"{nameof(model.Name)} is null or white space.");
            }

            if (_expenseCategoryService.Exist(model.Name))
            {
                return BadRequest($"Category {model.Name} is already exists.");
            }

            _expenseCategoryService.Update(model);

            return Ok("Expense category was updated");
        }

        /// <summary>
        /// Deletes an expense category.
        /// </summary>
        /// <param name="model">The expense category model to delete.</param>
        /// <returns>A success message if the category was deleted.</returns>
        /// <response code="200">The expense category was successfully deleted.</response>
        /// <response code="400">The model is null.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("delete-expense-category")]
        public IActionResult DeleteExpenseCategory([FromBody] ExpenseCategoryModel model)
        {
            if (model == null)
            {
                return BadRequest($"{nameof(model)} is null.");
            }

            _expenseCategoryService.Delete(model);

            return Ok("Expense category was deleted");
        }
    }
}
