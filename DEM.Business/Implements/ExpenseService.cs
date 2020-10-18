﻿using AutoMapper;
using DEM.EF;
using DEM.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DEM.App
{
    public class ExpenseService : IExpenseService
    {
        private ILogger<ExpenseService> _logger { get; set; }
        private readonly IUnitOfWorkMedia _unitOfWorfkMedia;
        private readonly IMapper _mapper;
        private readonly IPayerService _payerService;
        public ExpenseService(ILogger<ExpenseService> logger, IUnitOfWorkMedia unitOfWorkMedia, IMapper mapper, IPayerService payerService)
        {
            _logger = logger;
            _unitOfWorfkMedia = unitOfWorkMedia;
            _mapper = mapper;
            _payerService = payerService;
        }
        public bool Create(List<ExpenseDto> expenses)
        {
            var expenseEntities = _mapper.Map<List<ExpenseDto>, List<Expense>>(expenses);
            _unitOfWorfkMedia.ExpenseRepository.AddRange(expenseEntities);

            return _unitOfWorfkMedia.SaveChanges() > 0;
        }
        public bool Update(ExpenseDto data)
        {
            var expenseEntitie = _mapper.Map<ExpenseDto, Expense>(data);
            _unitOfWorfkMedia.ExpenseRepository.Update(expenseEntitie, UpdateAccessMode.DENY_UPDATE, "CreatedBy", "CreatedDate");

            return _unitOfWorfkMedia.SaveChanges() > 0;
        }
        public bool Delete(Guid expenseId)
        {
            _unitOfWorfkMedia.ExpenseRepository.Delete(expenseId);
            return _unitOfWorfkMedia.SaveChanges() > 0;
        }
        public List<ExpenseDto> LoadData(Guid categoryId, DateTime startTime, DateTime endTime)
        {
            var payers = _payerService.getData();
            var model = (from expense in _unitOfWorfkMedia.Expenses
                     join category in _unitOfWorfkMedia.Categories on expense.CategoryId equals category.Id
                     join payer in _unitOfWorfkMedia.Payers on expense.Payer equals payer.Code
                     where (expense.PayTime >= startTime.Date && expense.PayTime <= endTime.Date)
                     select  new ExpenseDto
                     {
                         Id =expense.Id,
                         CategoryId = category.Id,
                         CategoryName = category.Name,
                         Payer = payer.Code,
                         PayerName = payer.Name,
                         PayTime = expense.PayTime,
                         Money = expense.Money,
                         Description = expense.Description
                     }
                     ).ToList();

            return model;
        }        
        public ICollection<Payer> GetPayers() => _payerService.getData();

        public ExpenseDto GetData(Guid expenseId)
        {
            var expenseEntity = _unitOfWorfkMedia.ExpenseRepository.FindById(expenseId);
            //Map into ExpenseDto
            var expenseDto = _mapper.Map<Expense, ExpenseDto>(expenseEntity);
            return expenseDto;
        }

       
    }
}
