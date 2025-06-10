using System;
using System.Collections.Generic;
using Task_12.Entities;
using Task_12.Models;

namespace Task_12.Services.Mappers
{
    public class ExpenseCategoryMapper
    {
        public ExpenseCategoryEntity GetEntity(ExpenseCategoryModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), $"{nameof(model)} is null.");
            }

            return new ExpenseCategoryEntity()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public ExpenseCategoryModel GetModel(ExpenseCategoryEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} is null.");
            }

            return new ExpenseCategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public IReadOnlyList<ExpenseCategoryModel> GetModels(IReadOnlyList<ExpenseCategoryEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities), $"{nameof(entities)} is null.");
            }

            ExpenseCategoryModel[] models = new ExpenseCategoryModel[entities.Count];

            for (int i = 0; i < entities.Count; i++)
            {
                models[i] = GetModel(entities[i]);
            }

            return models;
        }
    }
}
