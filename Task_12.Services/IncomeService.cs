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
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IIncomeCategoryRepository _incomeCategoryRepository;

        private readonly IncomeMapper _incomeMapper;

        public IncomeService(IIncomeRepository incomeRepository, IIncomeCategoryRepository incomeCategoryRepository)
        {
            if (incomeRepository == null)
            {
                throw new ArgumentNullException(nameof(incomeRepository), $"{nameof(incomeRepository)} is null.");
            }

            if (incomeCategoryRepository == null)
            {
                throw new ArgumentNullException(nameof(incomeCategoryRepository), $"{nameof(incomeCategoryRepository)} is null.");
            }

            _incomeRepository = incomeRepository;
            _incomeCategoryRepository = incomeCategoryRepository;

            _incomeMapper = new IncomeMapper();
        }

        public IReadOnlyList<IncomeModel> GetAll()
        {
            return _incomeMapper.GetModels(_incomeRepository.GetAll(includeAdditionalData: true));
        }

        public IReadOnlyList<IncomeModel> GetIncomesByDate(DateOnly date)
        {
            IReadOnlyList<IncomeEntity> incomeEntities = _incomeRepository.Find(
                income => income.Date == date,
                includeAdditionalData: true
            );

            return _incomeMapper.GetModels(incomeEntities);
        }

        public IReadOnlyList<IncomeModel> GetIncomesByDateRange(DateOnly startDate, DateOnly endDate)
        {
            IReadOnlyList<IncomeEntity> incomeEntities = _incomeRepository.Find(
                income => income.Date >= startDate && income.Date <= endDate,
                includeAdditionalData: true
            );

            return _incomeMapper.GetModels(incomeEntities);
        }

        public void Add(IncomeModel incomeModel)
        {
            if (incomeModel == null)
            {
                throw new ArgumentNullException(nameof(incomeModel), $"{nameof(incomeModel)} is null.");
            }

            IncomeCategoryEntity categoryEntity = _incomeCategoryRepository.FirstOrDefault(category => category.Name == incomeModel.Category);

            if (categoryEntity == null)
            {
                throw new EntityNotFoundException($"Income category '{incomeModel.Category}' doesn't exist.");
            }

            _incomeRepository.Add(_incomeMapper.GetEntity(incomeModel, categoryEntity));
        }

        public void Delete(IncomeModel incomeModel)
        {
            if (incomeModel == null)
            {
                throw new ArgumentNullException(nameof(incomeModel), $"{nameof(incomeModel)} is null.");
            }

            IncomeEntity incomeEntity = _incomeRepository.FirstOrDefault(income => income.Id == incomeModel.Id, includeAdditionalData: false);

            if (incomeEntity == null)
            {
                throw new EntityNotFoundException($"Income doesn't exist.");
            }

            _incomeRepository.Delete(incomeEntity);
        }

        public void Update(IncomeModel incomeModel)
        {
            if (incomeModel == null)
            {
                throw new ArgumentNullException(nameof(incomeModel), $"{nameof(incomeModel)} is null.");
            }

            IncomeEntity incomeEntity = _incomeRepository.FirstOrDefault(income => income.Id == incomeModel.Id, includeAdditionalData: false);

            if (incomeEntity == null)
            {
                throw new EntityNotFoundException($"Income doesn't exist.");
            }

            IncomeCategoryEntity incomeCategory = _incomeCategoryRepository.FirstOrDefault(category => category.Name == incomeModel.Category);

            if (incomeCategory == null)
            {
                throw new EntityNotFoundException($"Income category '{incomeModel.Category}' doesn't exist.");
            }

            incomeEntity.Date = incomeModel.Date;
            incomeEntity.IncomeCategory = incomeCategory;
            incomeEntity.Amount = incomeModel.Amount;

            _incomeRepository.Update(incomeEntity);
        }
    }
}
