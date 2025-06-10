using System;
using System.Collections.Generic;
using Task_12.Entities;
using Task_12.Models;

namespace Task_12.Services.Mappers
{
    public class IncomeMapper
    {
        public IncomeModel GetModel(IncomeEntity incomeEntity)
        {
            if (incomeEntity == null)
            {
                throw new ArgumentNullException(nameof(incomeEntity), $"{nameof(incomeEntity)} is null.");
            }

            return new IncomeModel()
            {
                Id = incomeEntity.Id,
                Date = incomeEntity.Date,
                Amount = incomeEntity.Amount,
                Category = incomeEntity.IncomeCategory?.Name
            };
        }

        public IReadOnlyList<IncomeModel> GetModels(IReadOnlyList<IncomeEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities), $"{nameof(entities)} is null.");
            }

            IncomeModel[] models = new IncomeModel[entities.Count];

            for (int i = 0; i < entities.Count; i++)
            {
                models[i] = GetModel(entities[i]);
            }

            return models;
        }

        public IncomeEntity GetEntity(IncomeModel incomeModel, IncomeCategoryEntity incomeCategoryEntity)
        {
            if (incomeModel == null)
            {
                throw new ArgumentNullException(nameof(incomeModel), $"{nameof(incomeModel)} is null.");
            }

            if (incomeCategoryEntity == null)
            {
                throw new ArgumentNullException(nameof(incomeCategoryEntity), $"{nameof(incomeCategoryEntity)} is null.");
            }

            return new IncomeEntity()
            {
                Id = Guid.NewGuid(),
                Date = incomeModel.Date,
                Amount = incomeModel.Amount,
                IncomeCategory = incomeCategoryEntity
            };
        }
    }
}
