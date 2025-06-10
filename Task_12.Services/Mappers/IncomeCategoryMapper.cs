using System;
using System.Collections.Generic;
using Task_12.Entities;
using Task_12.Models;

namespace Task_12.Services.Mappers
{
    public class IncomeCategoryMapper
    {
        public IncomeCategoryEntity GetEntity(IncomeCategoryModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), $"{nameof(model)} is null.");
            }

            return new IncomeCategoryEntity()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public IncomeCategoryModel GetModel(IncomeCategoryEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} is null.");
            }

            return new IncomeCategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public IReadOnlyList<IncomeCategoryModel> GetModels(IReadOnlyList<IncomeCategoryEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities), $"{nameof(entities)} is null.");
            }

            IncomeCategoryModel[] models = new IncomeCategoryModel[entities.Count];

            for (int i = 0; i < entities.Count; i++)
            {
                models[i] = GetModel(entities[i]);
            }

            return models;
        }
    }
}
