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
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        private readonly ExpenseCategoryMapper _expenseCategoryMapper;

        public ExpenseCategoryService(IExpenseCategoryRepository expenseCategoryRepository)
        {
            if (expenseCategoryRepository == null)
            {
                throw new ArgumentNullException(nameof(expenseCategoryRepository), $"{nameof(expenseCategoryRepository)} is null.");
            }

            _expenseCategoryRepository = expenseCategoryRepository;
            _expenseCategoryMapper = new ExpenseCategoryMapper();
        }

        public IReadOnlyList<ExpenseCategoryModel> GetAll()
        {
            return _expenseCategoryMapper.GetModels(_expenseCategoryRepository.GetAll());
        }

        public bool Exist(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                throw new ArgumentException($"{nameof(categoryName)} is null or white space.");
            }

            return _expenseCategoryRepository.FirstOrDefault(category => category.Name == categoryName) != null;
        }

        public void Add(ExpenseCategoryModel expenseCategoryModel)
        {
            if (expenseCategoryModel == null)
            {
                throw new ArgumentNullException(nameof(expenseCategoryModel), $"{nameof(expenseCategoryModel)} is null.");
            }

            _expenseCategoryRepository.Add(_expenseCategoryMapper.GetEntity(expenseCategoryModel));
        }

        public void Delete(ExpenseCategoryModel expenseCategoryModel)
        {
            if (expenseCategoryModel == null)
            {
                throw new ArgumentNullException(nameof(expenseCategoryModel), $"{nameof(expenseCategoryModel)} is null.");
            }

            ExpenseCategoryEntity expenseCategoryEntity = _expenseCategoryRepository.FirstOrDefault(entity => entity.Id == expenseCategoryModel.Id);

            if (expenseCategoryEntity == null)
            {
                throw new EntityNotFoundException($"Expense category '{expenseCategoryModel.Name}' doesn't exist.");
            }

            _expenseCategoryRepository.Delete(expenseCategoryEntity);
        }

        public void Update(ExpenseCategoryModel expenseCategoryModel)
        {
            if (expenseCategoryModel == null)
            {
                throw new ArgumentNullException(nameof(expenseCategoryModel), $"{nameof(expenseCategoryModel)} is null.");
            }

            ExpenseCategoryEntity expenseCategoryEntity = _expenseCategoryRepository.FirstOrDefault(entity => entity.Id == expenseCategoryModel.Id);

            if (expenseCategoryEntity == null)
            {
                throw new EntityNotFoundException($"Expense category '{expenseCategoryModel.Name}' doesn't exist.");
            }

            expenseCategoryEntity.Name = expenseCategoryModel.Name;

            _expenseCategoryRepository.Update(expenseCategoryEntity);
        }
    }
}
