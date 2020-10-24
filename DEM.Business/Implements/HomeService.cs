using AutoMapper;
using DEM.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace DEM.App
{
    public class HomeService : BaseService, IHomeService
    {
        private ILogger<HomeService> _logger { get; set; }
        private readonly IUnitOfWorkMedia _unitOfWorfkMedia;
        private readonly IMapper _mapper;
        public HomeService(ILogger<HomeService> logger, IUnitOfWorkMedia unitOfWorkMedia, IMapper mapper) :base (logger, unitOfWorkMedia, mapper)
        {
            _logger = logger;
            _unitOfWorfkMedia = unitOfWorkMedia;
            _mapper = mapper;
        }

        public Tuple<ICollection<string>, List<List<decimal>>> GetDailyInMonthCurrent()
        {
            var datas = _unitOfWorfkMedia.GetDynamicResult("sp_DailyInMonthCurrent_Dashboard");
            var collectDaily = new List<string>();
            var collectMoneys = new List<List<decimal>>();
            var collectMoney_EXPENSE = new List<decimal>();
            var collectMoney_REVENUE = new List<decimal>();
            var collectMoney_SAVING = new List<decimal>();
            foreach (var item in datas)
            {
                collectDaily.Add(item.TimeDisplay);
                collectMoney_EXPENSE.Add(item.EXPENSE.Equals(DBNull.Value) ? 0 : item.EXPENSE);
                collectMoney_REVENUE.Add(item.REVENUE.Equals(DBNull.Value) ? 0 : item.REVENUE);
                collectMoney_SAVING.Add(item.SAVING.Equals(DBNull.Value) ? 0 : item.SAVING);
            }
            collectMoneys.Add(collectMoney_EXPENSE);
            collectMoneys.Add(collectMoney_REVENUE);
            collectMoneys.Add(collectMoney_SAVING);
            return new Tuple<ICollection<string>, List<List<decimal>>>(collectDaily, collectMoneys);
        }
        public Tuple<ICollection<string>, List<List<decimal>>, string> GetExpenseRealAndIntended()
        {
            var datas = _unitOfWorfkMedia.GetDynamicResult("sp_ExpenseRealAndIntended");
            var collectDaily = new List<string>();
            var collectMoneys = new List<List<decimal>>();
            var collectMoney_Expendse = new List<decimal>();
            var collectMoney_Intended = new List<decimal>();
            var description = "";
            foreach (var item in datas)
            {
                collectDaily.Add(item.Name);
                collectMoney_Expendse.Add(item.MoneyExpense.Equals(DBNull.Value) ? 0 : item.MoneyExpense);
                collectMoney_Intended.Add(item.MoneyIntended.Equals(DBNull.Value) ? 0 : item.MoneyIntended);
                description = item.Description;
            }

            collectMoneys.Add(collectMoney_Expendse);
            collectMoneys.Add(collectMoney_Intended);
            return new Tuple<ICollection<string>, List<List<decimal>>,string>(collectDaily, collectMoneys, description);
        }
        public Tuple<string, decimal, decimal, decimal,decimal> GetExpenseStatistical(DateTime formDate, DateTime toDate)
        {
            var datas = _unitOfWorfkMedia.GetDynamicResult("sp_ExpenseStatistical @p0, @p1",
                new Microsoft.Data.SqlClient.SqlParameter("@p0", formDate),
                new Microsoft.Data.SqlClient.SqlParameter("@p1", toDate));
            var data = datas.FirstOrDefault();
            return new Tuple<string, decimal, decimal, decimal, decimal>(
                data.ExpenseMaxName.Equals(DBNull.Value) ? "": data.ExpenseMaxName,
                data.ExpenseMaxMoney.Equals(DBNull.Value) ? 0 : data.ExpenseMaxMoney,
                data.Money_Total.Equals(DBNull.Value) ? 0 : data.Money_Total,
                data.Money_VK.Equals(DBNull.Value) ? 0 : data.Money_VK,
                data.Money_CK.Equals(DBNull.Value) ? 0 : data.Money_CK
                );
        }
    }
}
