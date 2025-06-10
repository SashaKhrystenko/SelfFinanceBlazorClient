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
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;

        private readonly ExpenseMapper _expenseMapper;

        public ExpenseService(IExpenseRepository expenseRepository, IExpenseCategoryRepository expenseCategoryRepository)
        {
            if (expenseRepository == null)
            {
                throw new ArgumentNullException(nameof(expenseRepository), $"{nameof(expenseRepository)} is null.");
            }

            if (expenseCategoryRepository == null)
            {
                throw new ArgumentNullException(nameof(expenseCategoryRepository), $"{nameof(expenseCategoryRepository)} is null.");
            }

            _expenseRepository = expenseRepository;
            _expenseCategoryRepository = expenseCategoryRepository;

            _expenseMapper = new ExpenseMapper();
        }

        public IReadOnlyList<ExpenseModel> GetAll()
        {
            return _expenseMapper.GetModels(_expenseRepository.GetAll(includeAdditionalData: true));
        }

        public IReadOnlyList<ExpenseModel> GetExpensesByDate(DateOnly date)
        {
            IReadOnlyList<ExpenseEntity> expenseEntities = _expenseRepository.Find(
                expense => expense.Date == date,
                includeAdditionalData: true
            );

            return _expenseMapper.GetModels(expenseEntities);
        }

        public IReadOnlyList<ExpenseModel> GetExpensesByDateRange(DateOnly startDate, DateOnly endDate)
        {
            IReadOnlyList<ExpenseEntity> expenseEntities = _expenseRepository.Find(
                expense =>
                    expense.Date >= startDate
                    && expense.Date <= endDate,
                includeAdditionalData: true
            );

            return _expenseMapper.GetModels(expenseEntities);
        }

        public void Add(ExpenseModel expenseModel)
        {
            if (expenseModel == null)
            {
                throw new ArgumentNullException(nameof(expenseModel), $"{nameof(expenseModel)} is null.");
            }

            ExpenseCategoryEntity expenseCategoryEntity = _expenseCategoryRepository.FirstOrDefault(category => category.Name == expenseModel.Category);

            if (expenseCategoryEntity == null)
            {
                throw new EntityNotFoundException($"Expense category '{expenseModel.Category}' doesn't exist.");
            }

            _expenseRepository.Add(_expenseMapper.GetEntity(expenseModel, expenseCategoryEntity));
        }

        public void Delete(ExpenseModel expenseModel)
        {
            if (expenseModel == null)
            {
                throw new ArgumentNullException(nameof(expenseModel), $"{nameof(expenseModel)} is null.");
            }

            ExpenseEntity expenseEntity = _expenseRepository.FirstOrDefault(expense => expense.Id == expenseModel.Id, includeAdditionalData: false);

            if (expenseEntity == null)
            {
                throw new EntityNotFoundException($"Expense doesn't exist.");
            }

            _expenseRepository.Delete(expenseEntity);
        }

        public void Update(ExpenseModel expenseModel)
        {
            if (expenseModel == null)
            {
                throw new ArgumentNullException(nameof(expenseModel), $"{nameof(expenseModel)} is null.");
            }

            ExpenseEntity expenseEntity = _expenseRepository.FirstOrDefault(expense => expense.Id == expenseModel.Id, includeAdditionalData: false);

            if (expenseEntity == null)
            {
                throw new EntityNotFoundException("Expense doesn't exist.");
            }

            ExpenseCategoryEntity expenseCategoryEntity = _expenseCategoryRepository.FirstOrDefault(category => category.Name == expenseModel.Category);

            if (expenseCategoryEntity == null)
            {
                throw new EntityNotFoundException($"Expense category '{expenseModel.Category}' doesn't exist.");
            }

            expenseEntity.Date = expenseModel.Date;
            expenseEntity.ExpenseCategory = expenseCategoryEntity;
            expenseEntity.Amount = expenseModel.Amount;

            _expenseRepository.Update(expenseEntity);
        }
    }
}
