using Microsoft.AspNetCore.Mvc;
using System;
using Task_12.Models;
using Task_12.Services.Interfaces;

namespace Task_12.Controllers
{
    [ApiController]
    [Route("incomes")]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IIncomeCategoryService _incomeCategoryService;

        public IncomeController(IIncomeService incomeService, IIncomeCategoryService incomeCategoryService)
        {
            if (incomeService == null)
            {
                throw new ArgumentNullException(nameof(incomeService), $"{nameof(incomeService)} is null.");
            }

            if (incomeCategoryService == null)
            {
                throw new ArgumentNullException(nameof(incomeCategoryService), $"{nameof(incomeCategoryService)} is null.");
            }

            _incomeService = incomeService;
            _incomeCategoryService = incomeCategoryService;
        }

        /// <summary>
        /// Retrieves all incomes.
        /// </summary>
        /// <returns>A list of income records.</returns>
        /// <response code="200">Returns the list of incomes.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("get-all-incomes")]
        public IActionResult GetAllIncomes()
        {
            return Ok(_incomeService.GetAll());
        }

        /// <summary>
        /// Adds a new income record.
        /// </summary>
        /// <param name="category">The income category.</param>
        /// <param name="amount">The amount of earned monney.</param>
        /// <returns>A success message if the income was added.</returns>
        /// <response code="200">The income was successfully added.</response>
        /// <response code="400">The model is null or invalid.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("add-income")]
        public IActionResult AddIncome([FromForm] string category, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return BadRequest($"{nameof(category)} is null or white space.");
            }

            if (!_incomeCategoryService.Exist(category))
            {
                return BadRequest($"Category {category} doesn't exist.");
            }

            if (amount <= 0)
            {
                return BadRequest($"{nameof(amount)} can't be less than zero.");
            }

            IncomeModel model = new()
            {
                Id = Guid.NewGuid(),
                Date = DateOnly.FromDateTime(DateTime.Now),
                Category = category,
                Amount = amount
            };

            _incomeService.Add(model);

            return Ok("Income was added.");
        }

        /// <summary>
        /// Updates an existing income record.
        /// </summary>
        /// <param name="model">The updated income model.</param>
        /// <returns>A success message if the income was updated.</returns>
        /// <response code="200">The income was successfully updated.</response>
        /// <response code="400">The model is null or invalid.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch("update-income")]
        public IActionResult UpdateIncome([FromBody] IncomeModel model)
        {
            if (model == null)
            {
                return BadRequest($"{nameof(model)} is null.");
            }

            if (string.IsNullOrWhiteSpace(model.Category))
            {
                return BadRequest($"{nameof(model.Category)} is null or white space.");
            }

            if (!_incomeCategoryService.Exist(model.Category))
            {
                return BadRequest($"Category {model.Category} doesn't exist.");
            }

            if (model.Amount <= 0)
            {
                return BadRequest($"{nameof(model.Amount)} can't be less than zero.");
            }

            _incomeService.Update(model);

            return Ok("Income was updated.");
        }

        /// <summary>
        /// Deletes an income record.
        /// </summary>
        /// <param name="model">The income model to delete.</param>
        /// <returns>A success message if the income was deleted.</returns>
        /// <response code="200">The income was successfully deleted.</response>
        /// <response code="400">The model is null or invalid.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("delete-income")]
        public IActionResult DeleteIncome([FromBody] IncomeModel model)
        {
            if (model == null)
            {
                return BadRequest($"{nameof(model)} is null.");
            }

            _incomeService.Delete(model);

            return Ok("Income was deleted.");
        }
    }
}
