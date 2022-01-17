using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.AmountOfSalary;
using EmployeeMG.Core.Core;
using EmployeeMG.Data.Context;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMG.Core.Application.AmountOfSalary
{
    public class AmountOfSalaryApplication : IAmountOfSalaryApplication
    {

        private readonly ApplicationContext _applicationContext;

        public AmountOfSalaryApplication(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }


        public async Task<ListAmountOfSalary> GetAll(int pageId, int take, string FilterUnitName)
        {
            try
            {
                var result = _applicationContext.AmountOfSalary.Select(c => new AmountOfSalaryModel()
                {
                    Id = c.Id.ToString(),
                    Salary = c.Salary,
                    UnitName = c.TUnit.Name,
                    UnitId =c.UnitId.ToString()
                });

                if (!string.IsNullOrWhiteSpace(FilterUnitName))
                    result = result.Where(c => c.UnitName==FilterUnitName);

                var skip = (pageId - 1) * take;

                var model = new ListAmountOfSalary()
                {
                    FilterUnitName = FilterUnitName,
                    AmountOfSalaryModels = await result.OrderByDescending(c => c.Salary).Skip(skip).Take(take).ToListAsync(),
                };

                model.GenaratPaging(result, pageId, take);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> Create(CreateAmountOfSalary amountOfSalary)
        {
            OperationResult operationResult = new OperationResult();
            try
            {

                Random r= new Random();
                var id = r.Next(1111111, 9999999);
                var _Am = new TAmountOfSalary()
                {
                    Id = id,
                    UnitId = Convert.ToInt32(amountOfSalary.UnitId),
                    Salary = amountOfSalary.Salary,
                    Insurance = amountOfSalary.Insurance,
                    RightChild = amountOfSalary.RightChild,
                    RightHousing = amountOfSalary.RightHousing,
                    HourOfOverTime = amountOfSalary.HourOfOverTime,
                };

                if (await _applicationContext.AmountOfSalary.AnyAsync(c =>
                    c.UnitId == _Am.UnitId))
                    return operationResult.Failed("برای این واحد قبلا میزان حقوق درج شده");

                await _applicationContext.AmountOfSalary.AddAsync(_Am);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }


        public async Task<EditAmountOfSalary> GetForEdit(string Id)
        {
            try
            {
                var _am = await _applicationContext.AmountOfSalary.Where(c => c.Id == Convert.ToInt32(Id))
                    .Select(c => new EditAmountOfSalary()
                    {
                        Id = c.Id.ToString(),
                        UnitId = c.UnitId.ToString(),
                        UnitName = c.TUnit.Name,
                        RightHousing = c.RightHousing,
                        HourOfOverTime = c.HourOfOverTime,
                        Salary = c.Salary,
                        RightChild = c.RightChild,
                        Insurance = c.Insurance,
                    }).SingleOrDefaultAsync();


                return _am;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> Edit(EditAmountOfSalary amountOfSalary)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var _am = await _applicationContext.AmountOfSalary.FindAsync(Convert.ToInt32(amountOfSalary.Id));

                if (_am is null)
                    return operationResult.Failed();

                _am.UnitId = Convert.ToInt32(amountOfSalary.UnitId);
                _am.HourOfOverTime = amountOfSalary.HourOfOverTime;
                _am.Insurance = amountOfSalary.Insurance;
                _am.Salary = amountOfSalary.Salary;
                _am.RightHousing = amountOfSalary.RightHousing;
                _am.RightChild = amountOfSalary.RightChild;

                if (await _applicationContext.AmountOfSalary.AnyAsync(c => c.UnitId == _am.UnitId && c.Id != _am.Id))
                    return operationResult.Failed("برای این واحد قبلا میزان حقوق درج شده");

                _applicationContext.AmountOfSalary.Update(_am);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }


        public async Task<OperationResult> Delete(string Id)
        {
            OperationResult operationResult =new OperationResult();

            try
            {
                var _am = await _applicationContext.AmountOfSalary.FindAsync(Convert.ToInt32(Id));

                if (_am is null)
                    return operationResult.Failed();

                _applicationContext.AmountOfSalary.Remove(_am);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }
    }
}
