using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Unit;
using EmployeeMG.Core.Core;
using EmployeeMG.Data.Context;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMG.Core.Application.Unit
{
    public class UnitApplication : IUnitApplication
    {

        private readonly ApplicationContext _applicationContext;

        public UnitApplication(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }


        public async Task<ListUnit> GetAll(int pageId, int take, string filter)
        {
            try
            {
                var result = _applicationContext.Unit.Select(c => new UnitModel()
                {
                    Code = c.Code.ToString(),
                    Name = c.Name,
                });

                if (!string.IsNullOrWhiteSpace(filter))
                    result = result.Where(c => c.Name.Contains(filter) || c.Code.Contains(filter));

                var skip = (pageId - 1) * take;

                var model = new ListUnit()
                {
                    Filter = filter,
                    UnitModels = await result.OrderByDescending(c => c.Name).Skip(skip).Take(take).ToListAsync()
                };

                model.GenaratPaging(result, pageId, take);
                return model;

            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<OperationResult> Create(string Name)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                Random r = new Random();
                var code = r.Next(1111, 9999);

                var _unit = new TUnit()
                {
                    Code = code,
                    Name = Name
                };

                if (await _applicationContext.Unit.AnyAsync(c => c.Name == _unit.Name))
                    return operationResult.Failed("این واحد قبلا درج شده");

                await _applicationContext.Unit.AddAsync(_unit);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();


            }
            catch (Exception)
            {
                return operationResult.Failed();
            }


        }

        public async Task<OperationResult> Delete(string id)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _Unit = await _applicationContext.Unit.FindAsync(Convert.ToInt32(id));
                if (_Unit is null)
                    return operationResult.Failed();

                _applicationContext.Unit.Remove(_Unit);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<UnitModel> GetForEdit(string Id)
        {
            try
            {
                var _unit = await _applicationContext.Unit.Where(c => c.Code == Convert.ToInt32(Id)).Select(c => new UnitModel()
                {
                    Name = c.Name,
                    Code = c.Code.ToString()
                }).SingleOrDefaultAsync();

                return _unit;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> Edit(UnitModel unitModel)
        {
            OperationResult operationResult  =new OperationResult();

            try
            {
                var _unit = await _applicationContext.Unit.FindAsync(Convert.ToInt32(unitModel.Code));

                if (_unit is null)
                    return operationResult.Failed();

                if (await _applicationContext.Unit.AnyAsync(c =>
                    c.Name == unitModel.Name && c.Code != Convert.ToInt32(unitModel.Code)))
                    return operationResult.Failed("این واحد وجود دارد");

                _unit.Name = unitModel.Name;


                _applicationContext.Unit.Update(_unit);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<List<UnitModel>> GetForPersonnel()
        {
            var _unit = await _applicationContext.Unit.Select(c => new UnitModel()
            {
                Name = c.Name,
                Code = c.Code.ToString()

            }).ToListAsync();

            return _unit;
        }
    }
}
