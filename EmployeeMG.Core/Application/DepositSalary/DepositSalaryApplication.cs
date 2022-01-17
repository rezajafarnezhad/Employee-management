using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.DepositSalary;
using EmployeeMG.Core.Core;
using EmployeeMG.Data.Context;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMG.Core.Application.DepositSalary
{
    public class DepositSalaryApplication : IDepositSalaryApplication
    {
        private readonly ApplicationContext _applicationContext;

        public DepositSalaryApplication(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<ListDepositSalary> GetAll(int pageId, int take, int filtercodePersonnel, string filterMonth)
        {
            try
            {
                var result = _applicationContext.DepositSalary.Select(c => new DepositSalaryModel()
                {
                    Id = Convert.ToInt32(c.Id),
                    Year = c.Year,
                    Month = c.Month,
                    Amount = c.Amount,
                    PersonnelCode = c.PersonnelCode
                });

                if (!string.IsNullOrWhiteSpace(filterMonth))
                    result = result.Where(c => c.Month == filterMonth);

                if (filtercodePersonnel != 0)
                    result = result.Where(c => c.PersonnelCode == filtercodePersonnel);

                var skip = (pageId - 1) * take;

                var model = new ListDepositSalary()
                {
                    FilterCodePersonnel = filtercodePersonnel,
                    FilterMonth = filterMonth,
                    DepositSalaryModels = await result.OrderByDescending(c => c.Id).Skip(skip).Take(take).ToListAsync(),
                };

                model.GenaratPaging(result, pageId, take);
                return model;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> Create(string Month)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var dateNow = DateTime.Now.ToFarsi();
                var _Year = dateNow.Split("/")[0];

                if (await _applicationContext.DepositSalary.AnyAsync(c => c.Year == _Year && c.Month == Month))
                    return operationResult.Failed($"حقوق ماه {Month} پرداخت شده است");

                var Personnels = await _applicationContext.Employee.ToListAsync();

                var AmountOfSalary = await _applicationContext.AmountOfSalary.ToListAsync();

                List<TDepositSalary> depositSalaries = new List<TDepositSalary>();

                foreach (var Item in Personnels)
                {
                    var amountTotal = 0;

                    foreach (var Item2 in AmountOfSalary.Where(c => c.UnitId == Item.Unitid))
                    {
                        if (Item.Unitid == Item2.UnitId)
                        {
                            amountTotal = Item2.Salary + Item2.HourOfOverTime + Item2.Insurance + Item2.RightChild +
                                          Item2.RightHousing;
                        }
                    }
                    var depositSalary = new TDepositSalary()
                    {
                        PersonnelCode = Item.PersonnelCode,
                        Month = Month,
                        Year = _Year,
                        Amount = Convert.ToInt64(amountTotal),
                    };

                    depositSalaries.Add(depositSalary);
                }

                await AddDepositSalary(depositSalaries);
                return operationResult.Succeeded();
            }
            catch (Exception e)
            {
                return operationResult.Failed(e);
            }

        }

        private async Task AddDepositSalary(List<TDepositSalary> depositSalaries)
        {

            await _applicationContext.AddRangeAsync(depositSalaries);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
