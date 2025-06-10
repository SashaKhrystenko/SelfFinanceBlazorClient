using System;
using System.Collections.Generic;
using Task_12.Entities;
using Task_12.Models;
using Task_12.Repositories.Interfaces;
using Task_12.Services.Exceptions;
using Task_12.Services.Interfaces;
using Task_12.Services.Mappers;

namespace Task_12.Services
{
    public class IncomeCategoryService : IIncomeCategoryService
    {
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;

        private readonly IncomeCategoryMapper _incomeCategoryMapper;

        public IncomeCategoryService(IIncomeCategoryRepository incomeCategoryRepository)
        {
            if (incomeCategoryRepository == null)
            {
                throw new ArgumentNullException(nameof(incomeCategoryRepository), $"{nameof(incomeCategoryRepository)} is null.");
            }

            _incomeCategoryRepository = incomeCategoryRepository;
            _incomeCategoryMapper = new IncomeCategoryMapper();
        }

        public IReadOnlyList<IncomeCategoryModel> GetAll()
        {
            return _incomeCategoryMapper.GetModels(_incomeCategoryRepository.GetAll());
        }

        public bool Exist(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                throw new ArgumentException($"{nameof(categoryName)} is null or white space.");
            }

            return _incomeCategoryRepository.FirstOrDefault(category => category.Name == categoryName) != null;
        }

        public void Add(IncomeCategoryModel incomeCategoryModel)
        {
            if (incomeCategoryModel == null)
            {
                throw new ArgumentNullException(nameof(incomeCategoryModel), $"{nameof(incomeCategoryModel)} is null.");
            }

            _incomeCategoryRepository.Add(_incomeCategoryMapper.GetEntity(incomeCategoryModel));
        }

        public void Delete(IncomeCategoryModel incomeCategoryModel)
        {
            if (incomeCategoryModel == null)
            {
                throw new ArgumentNullException(nameof(incomeCategoryModel), $"{nameof(incomeCategoryModel)} is null.");
            }

            IncomeCategoryEntity incomeCategoryEntity = _incomeCategoryRepository.FirstOrDefault(entity => entity.Id == incomeCategoryModel.Id);

            if (incomeCategoryEntity == null)
            {
                throw new EntityNotFoundException($"Income category '{incomeCategoryEntity.Name}' doesn't exist.");
            }

            _incomeCategoryRepository.Delete(incomeCategoryEntity);
        }

        public void Update(IncomeCategoryModel incomeCategoryModel)
        {
            if (incomeCategoryModel == null)
            {
                throw new ArgumentNullException(nameof(incomeCategoryModel), $"{nameof(incomeCategoryModel)} is null.");
            }

            IncomeCategoryEntity incomeCategoryEntity = _incomeCategoryRepository.FirstOrDefault(entity => entity.Id == incomeCategoryModel.Id);

            if (incomeCategoryEntity == null)
            {
                throw new EntityNotFoundException($"Income category '{incomeCategoryEntity.Name}' doesn't exist.");
            }

            incomeCategoryEntity.Name = incomeCategoryModel.Name;

            _incomeCategoryRepository.Update(incomeCategoryEntity);
        }
    }
}
