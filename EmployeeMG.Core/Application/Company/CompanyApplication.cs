using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Company;
using EmployeeMG.Core.Core;
using EmployeeMG.Data.Context;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMG.Core.Application.Company
{
    public class CompanyApplication : ICompanyApplication
    {

        private readonly ApplicationContext _applicationContext;
        private readonly IUploadFile _uploadFile;

        public CompanyApplication(ApplicationContext applicationContext, IUploadFile uploadFile)
        {
            _applicationContext = applicationContext;
            _uploadFile = uploadFile;
        }

        public async Task<List<ListCompany>> ListCompany()
        {

            try
            {

                var _Companys = await _applicationContext.Company.Select(c => new ListCompany()
                {
                    RegistrationCode = c.RegistrationNumber,
                    Phone = c.Phone,
                    Logo = c.Logo,
                    Name = c.CompanyName,
                    Address = c.Address,
                    WebSite = c.WebSite

                }).ToListAsync();

                return _Companys;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> Create(Create company)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _Company = new TCompany()
                {
                    RegistrationNumber = company.RegistrationCode,
                    Phone = company.Phone,
                    WebSite = company.WebSite,
                    Address = company.Address,
                    CompanyName = company.Name
                };

                if (company.FileLogo is null)
                    return operationResult.Failed("لوگو وارد شود");

                _Company.Logo = _uploadFile.Upload(company.FileLogo);

                await _applicationContext.Company.AddAsync(_Company);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }

        }

        public async Task<bool> CompanyIsExists()
        {

            if (await _applicationContext.Company.AnyAsync())
                return true;

            return false;

        }

        public async Task<Edit> GetCompany(string Id)
        {
            var _company = await _applicationContext.Company.Where(c=>c.RegistrationNumber == Id).Select(c => new Edit()
            {
                WebSite = c.WebSite,
                Name = c.CompanyName,
                Phone = c.Phone,
                Address = c.Address,
                Logo = c.Logo,
                RegistrationCode = c.RegistrationNumber,
                RegistrationCode0 = c.RegistrationNumber,
                FileLogo = null

            }).SingleOrDefaultAsync();

            return _company;
        }

        public async Task<OperationResult> Edit(Edit edit)
        {
            OperationResult operationResult = new OperationResult();
            try
            {

                var _company = await _applicationContext.Company.FindAsync(edit.RegistrationCode0);

                if (_company is null)
                    return operationResult.Failed();

                _company.WebSite = edit.WebSite;
                _company.Address = edit.Address;
                _company.CompanyName = edit.Name;
                _company.RegistrationNumber = edit.RegistrationCode;
                _company.Phone = edit.Phone;

                if (edit.FileLogo != null)
                {
                    _uploadFile.DeleteFile(_company.Logo);
                 
                   _company.Logo= _uploadFile.Upload(edit.FileLogo);
                }

                _company.Edit(_company.RegistrationNumber,_company.CompanyName,_company.Phone,_company.WebSite,_company.Address,_company.Logo);
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
            OperationResult operationResult= new OperationResult();

            var _company = await _applicationContext.Company.FindAsync(Id);

            if (_company is null)
                return operationResult.Failed();


            if(_company.Logo !=null)
                _uploadFile.DeleteFile(_company.Logo);

             _applicationContext.Company.Remove(_company);
             await _applicationContext.SaveChangesAsync();

            return operationResult.Succeeded();
        }

    }
}
