using System;
using System.Collections.Generic;
using Task_12.Entities;
using Task_12.Models;

namespace Task_12.Services.Mappers
{
    public class ExpenseMapper
    {
        public ExpenseModel GetModel(ExpenseEntity expenseEntity)
        {
            if (expenseEntity == null)
            {
                throw new ArgumentNullException(nameof(expenseEntity), $"{nameof(expenseEntity)} is null.");
            }

            return new ExpenseModel()
            {
                Id = expenseEntity.Id,
                Date = expenseEntity.Date,
                Amount = expenseEntity.Amount,
                Category = expenseEntity.ExpenseCategory?.Name
            };
        }

        public IReadOnlyList<ExpenseModel> GetModels(IReadOnlyList<ExpenseEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities), $"{nameof(entities)} is null.");
            }

            ExpenseModel[] models = new ExpenseModel[entities.Count];

            for (int i = 0; i < entities.Count; i++)
            {
                models[i] = GetModel(entities[i]);
            }

            return models;
        }

        public ExpenseEntity GetEntity(ExpenseModel expenseModel, ExpenseCategoryEntity expenseCategoryEntity)
        {
            if (expenseModel == null)
            {
                throw new ArgumentNullException(nameof(expenseModel), $"{nameof(expenseModel)} is null.");
            }

            if (expenseCategoryEntity == null)
            {
                throw new ArgumentNullException(nameof(expenseCategoryEntity), $"{nameof(expenseCategoryEntity)} is null.");
            }

            return new ExpenseEntity()
            {
                Id = Guid.NewGuid(),
                Date = expenseModel.Date,
                Amount = expenseModel.Amount,
                ExpenseCategory = expenseCategoryEntity
            };
        }
    }
}
