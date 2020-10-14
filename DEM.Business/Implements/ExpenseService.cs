using AutoMapper;
using DEM.EF;
using DEM.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DEM.App
{
    public class ExpenseService : IExpenseService
    {
        private ILogger<ExpenseService> _logger { get; set; }
        private readonly IUnitOfWorkMedia _unitOfWorfkMedia;
        private readonly IMapper _mapper;
        public ExpenseService(ILogger<ExpenseService> logger, IUnitOfWorkMedia unitOfWorkMedia, IMapper mapper)
        {
            _logger = logger;
            _unitOfWorfkMedia = unitOfWorkMedia;
            _mapper = mapper;
        }
        public bool Create(List<ExpenseDto> expenses)
        {
            var expenseEntities = _mapper.Map<List<ExpenseDto>, List<Expense>>(expenses);
            _unitOfWorfkMedia.ExpenseRepository.AddRange(expenseEntities);

            return _unitOfWorfkMedia.SaveChanges() > 0;
        }
    }
}
