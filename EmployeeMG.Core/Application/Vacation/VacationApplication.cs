using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Employee;
using EmployeeMG.Core.ApplicationContract.Vacation;
using EmployeeMG.Core.Core;
using EmployeeMG.Data.Context;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EmployeeMG.Core.Application.Vacation
{
    public class VacationApplication : IVacationApplication
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IEmployeeApplication _employeeApplication;
        public VacationApplication(ApplicationContext applicationContext, IEmployeeApplication employeeApplication)
        {
            _applicationContext = applicationContext;
            _employeeApplication = employeeApplication;
        }

        public async Task<ListVacation> GetAll(int pageId, int take, int FilterCodePersonnel, string FilterFromDate)
        {
            try
            {
                var result = _applicationContext.Vacation.Select(c => new VacationModel()
                {
                    Id = c.Id.ToString(),
                    FromDate = c.FromDate,
                    CountDate = c.CountDate,
                    PersonnelCode = c.PersonnelCode
                });

                if (FilterCodePersonnel !=0)
                    result = result.Where(c => c.PersonnelCode ==FilterCodePersonnel);

                if (!string.IsNullOrWhiteSpace(FilterFromDate))
                    result = result.Where(c => c.FromDate== FilterFromDate);


                var skip = (pageId - 1) * take;

                var model = new ListVacation()
                {

                    FilterFromDate = FilterFromDate,
                    FilretCodePersonnel = FilterCodePersonnel,
                    VacationModels = await result.OrderByDescending(c => c.Id).Skip(skip).Take(take).ToListAsync(),
                };

                model.GenaratPaging(result, pageId, take);
                return model;

            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<OperationResult> CreateVacation(CreateVacation vacation)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                if (!await _employeeApplication.IsExistPersonnelCode(vacation.PersonnelCode))
                {
                    return operationResult.Failed("کد پرسنلی اشتباه است");
                }

                if (!await _employeeApplication.IsExistPersonnelCode(vacation.PersonnelCodeSuccessor))
                {
                    return operationResult.Failed("کد پرسنلی جانشین اشتباه است");

                }


                Random r = new Random();
                var _id = r.Next(11111, 99999);
                var _Vacation = new TVacation()
                {
                    Id = _id,
                    FromDate = vacation.FromDate.ToGeorgianDateTime().ToFarsi(),
                    PersonnelCode = Convert.ToInt32(vacation.PersonnelCode),
                    PersonnelCodeSuccessor = Convert.ToInt32(vacation.PersonnelCodeSuccessor),
                    CountDate = vacation.CountDate,
                    inmonth = vacation.FromDate.ToGeorgianDateTime().AddMonths(1).ToFarsi()
                };

                if (!await _applicationContext.Vacation.AnyAsync(c => c.PersonnelCode == _Vacation.PersonnelCode))
                {
                    await _applicationContext.Vacation.AddAsync(_Vacation);
                    await _applicationContext.SaveChangesAsync();
                    return operationResult.Succeeded();
                }

                var _fromdate = _Vacation.FromDate.ToGeorgianDateTime();
                var _inmonth = _Vacation.inmonth.ToGeorgianDateTime();

                var _Vacationm = await _applicationContext.Vacation.Where(c => c.PersonnelCode == _Vacation.PersonnelCode).ToListAsync();

                int all = 0;
                foreach (var item in _Vacationm)
                {

                    if (_Vacation.FromDate.ToGeorgianDateTime() >= item.FromDate.ToGeorgianDateTime() &&
                        _Vacation.FromDate.ToGeorgianDateTime() <= item.inmonth.ToGeorgianDateTime())
                    {
                        all += item.CountDate;
                    }

                }

                if (all > 4 || (vacation.CountDate + all) > 4)
                    return operationResult.Failed("این کارمند از تمام مرخصی های خود استفاده کرده کارمندان در هر سی روز 4 بار میتوانند از مرخصی استفاده کنند");

                await _applicationContext.Vacation.AddAsync(_Vacation);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<OperationResult> Delete(string ID)
        {
            OperationResult operationResult = new OperationResult();

            var _vacation = await _applicationContext.Vacation.FindAsync(Convert.ToInt32(ID));
            if (_vacation is null)
                return operationResult.Failed();
            
            _applicationContext.Vacation.Remove(_vacation);
            await _applicationContext.SaveChangesAsync();
            return operationResult.Succeeded();
        }
    }
}
