using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.BankAccount;
using EmployeeMG.Core.Core;
using EmployeeMG.Data.Context;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMG.Core.Application.BankAccount
{
    public class BankAccountApplication : IBankAccountApplication
    {
        private readonly ApplicationContext _applicationContext;

        public BankAccountApplication(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<ListBankAccount> GetAll(int pageId, int take, int FilterCodepersonnel,
            string FilterCodeShaba)
        {
            try
            {
                var result = _applicationContext.BankAccount.Select(c => new BankAccountModel()
                {
                    Id = c.Id.ToString(),
                    PersonnelCode = c.PersonnelCode,
                    Shaba = c.Shaba
                });

                if (FilterCodepersonnel != 0)
                    result = result.Where(c => c.PersonnelCode == FilterCodepersonnel);

                if (!string.IsNullOrWhiteSpace(FilterCodeShaba))
                    result = result.Where(c => c.Shaba.Contains(FilterCodeShaba));


                var skip = (pageId - 1) * take;

                var model = new ListBankAccount()
                {
                    FilterCodePersonnel = FilterCodepersonnel,
                    FilterCodeshaba = FilterCodeShaba,
                    BankAccountModels = await result.OrderByDescending(c => c.Id).Skip(skip).Take(take).ToListAsync(),
                };

                model.GenaratPaging(result, pageId, take);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> Create(int PersonnelCode, string CodeShaba)
        {
            OperationResult operationResult = new OperationResult();

            try
            {

                if (!await _applicationContext.Employee.AnyAsync(c => c.PersonnelCode == Convert.ToInt32(PersonnelCode)))
                    return operationResult.Failed("کد پرسنلی اشتباه است");

                if (await _applicationContext.BankAccount.AnyAsync(c => c.PersonnelCode == Convert.ToInt32(PersonnelCode)))
                    return operationResult.Failed("برای این کارمند حساب بانکی قبلا ثبت شده است برای هر کارمند تنها یک حساب بانکی میتوانید ثبت کنید");

                Random r =new Random();
                var id = r.Next(111111, 999999);
                var _BankAc = new TBankAccount()
                {
                    Id = id,
                    PersonnelCode = Convert.ToInt32(PersonnelCode),
                    Shaba = CodeShaba,
                };

                await _applicationContext.BankAccount.AddAsync(_BankAc);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }


        public async Task<BankAccountModel> GetForEdit(string id)
        {
            try
            {
                var _BankAc = await _applicationContext.BankAccount.Where(c => c.Id == Convert.ToInt32(id)).Select(c => new BankAccountModel()
                {
                    Id = c.Id.ToString(),
                    Shaba = c.Shaba,
                    PersonnelCode = c.PersonnelCode,
                }).SingleOrDefaultAsync();

                return _BankAc;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> Edit(BankAccountModel accountModel)
        {
            OperationResult operationResult = new OperationResult();


            try
            {

                if (!await _applicationContext.Employee.AnyAsync(c => c.PersonnelCode == Convert.ToInt32(accountModel.PersonnelCode)))
                    return operationResult.Failed("کد پرسنلی اشتباه است");

                var _BankAcc = await _applicationContext.BankAccount.FindAsync(Convert.ToInt32(accountModel.Id));

                if (_BankAcc is null)
                    return operationResult.Failed();

                if (await _applicationContext.BankAccount.AnyAsync(c =>
                    c.PersonnelCode == Convert.ToInt32(accountModel.PersonnelCode) &&
                    c.Id != Convert.ToInt32(accountModel.Id)))
                    return operationResult.Failed("برای این کارمند حساب بانکی قبلا ثبت شده است برای هر کارمند تنها یک حساب بانکی میتوانید ثبت کنید");

                _BankAcc.PersonnelCode = Convert.ToInt32(accountModel.PersonnelCode);
                _BankAcc.Shaba = accountModel.Shaba;

                _applicationContext.BankAccount.Update(_BankAcc);
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
            OperationResult operationResult = new OperationResult();


            try
            {
                var _BankAcc = await _applicationContext.BankAccount.FindAsync(Convert.ToInt32(Id));

                if (_BankAcc is null)
                    return operationResult.Failed();

                _applicationContext.BankAccount.Remove(_BankAcc);
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
