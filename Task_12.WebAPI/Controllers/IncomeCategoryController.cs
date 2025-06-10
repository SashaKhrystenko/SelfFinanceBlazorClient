using Microsoft.AspNetCore.Mvc;
using System;
using Task_12.Models;
using Task_12.Services.Interfaces;

namespace Task_12.Controllers
{
    [ApiController]
    [Route("income-categories")]
    public class IncomeCategoryController : ControllerBase
    {
        private readonly IIncomeCategoryService _incomeCategoryService;

        public IncomeCategoryController(IIncomeCategoryService incomeCategoryService)
        {
            if (incomeCategoryService == null)
            {
                throw new ArgumentNullException(nameof(incomeCategoryService), $"{nameof(incomeCategoryService)} is null.");
            }

            _incomeCategoryService = incomeCategoryService;
        }

        /// <summary>
        /// Retrieves all income categories.
        /// </summary>
        /// <returns>A list of income categories.</returns>
        /// <response code="200">Returns the list of income categories.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("get-all-income-categories")]
        public IActionResult GetAllExpenseCategories()
        {
            return Ok(_incomeCategoryService.GetAll());
        }

        /// <summary>
        /// Adds a new income category.
        /// </summary>
        /// <param name="name">The income category name.</param>
        /// <returns>A success message if the category was added.</returns>
        /// <response code="200">The income category was successfully added.</response>
        /// <response code="400">Entered invalid data</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("add-income-category")]
        public IActionResult AddExpenseCategory([FromForm] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest($"{nameof(name)} is null or white space.");
            }

            if (_incomeCategoryService.Exist(name))
            {
                return BadRequest($"Category {name} is already exists.");
            }

            IncomeCategoryModel model = new()
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            _incomeCategoryService.Add(model);

            return Ok("Income category was added");
        }

        /// <summary>
        /// Updates an existing income category.
        /// </summary>
        /// <param name="model">The updated income category model.</param>
        /// <returns>A success message if the category was updated.</returns>
        /// <response code="200">The income category was successfully updated.</response>
        /// <response code="400">The model is null or invalid.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPatch("update-income-category")]
        public IActionResult UpdateExpenseCategory([FromBody] IncomeCategoryModel model)
        {
            if (model == null)
            {
                return BadRequest($"{nameof(model)} is null.");
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest($"{model.Name} is null or white space.");
            }

            if (_incomeCategoryService.Exist(model.Name))
            {
                return BadRequest($"Category {model.Name} is already exists.");
            }

            _incomeCategoryService.Update(model);

            return Ok("Income category was updated");
        }

        /// <summary>
        /// Deletes an income category.
        /// </summary>
        /// <param name="model">The income category model to delete.</param>
        /// <returns>A success message if the category was deleted.</returns>
        /// <response code="200">The income category was successfully deleted.</response>
        /// <response code="400">The model is null.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("delete-income-category")]
        public IActionResult DeleteExpenseCategory([FromBody] IncomeCategoryModel model)
        {
            if (model == null)
            {
                return BadRequest($"{nameof(model)} is null.");
            }

            _incomeCategoryService.Delete(model);

            return Ok("Income category was deleted");
        }
    }
}
