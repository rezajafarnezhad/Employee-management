using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Employee;
using EmployeeMG.Core.Core;
using EmployeeMG.Data.Context;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMG.Core.Application.Personnel
{
    public class EmployeeApplication : IEmployeeApplication
    {

        private readonly ApplicationContext _applicationContext;
        private readonly IUploadFile _uploadFile;
        public EmployeeApplication(ApplicationContext applicationContext, IUploadFile uploadFile)
        {
            _applicationContext = applicationContext;
            _uploadFile = uploadFile;
        }

        public async Task<ListEmployee> GetAll(int pageId, int take, string filterName, string filterCodeMelli,
            string filterCodePersonnle)
        {

            try
            {
                var result = _applicationContext.Employee.Select(c => new EmployeeModel()
                {
                    CodeMelli = c.CodeMelli,
                    CodePersonnel = c.PersonnelCode.ToString(),
                    Firstname = c.FirstName,
                    Lastname = c.LastName,
                    place = c.Place,
                    DateOfEmployment = c.DateOfEmployment
                });

                if (!string.IsNullOrWhiteSpace(filterName))
                    result = result.Where(c => c.Firstname.Contains(filterName) || c.Lastname.Contains(filterName));

                if (!string.IsNullOrWhiteSpace(filterCodeMelli))
                    result = result.Where(c => c.CodeMelli.Contains(filterCodeMelli));

                if (!string.IsNullOrWhiteSpace(filterCodePersonnle))
                    result = result.Where(c => c.CodePersonnel.Contains(filterCodePersonnle));


                var skip = (pageId - 1) * take;

                var model = new ListEmployee()
                {
                    FilterCodeMelli = filterCodeMelli,
                    FilterCodePersonnel = filterCodePersonnle,
                    FilterName = filterName,
                    EmployeeModels = await result.OrderByDescending(c => c.Lastname).Skip(skip).Take(take).ToListAsync(),
                };

                model.GenaratPaging(result, pageId, take);
                return model;


            }
            catch (Exception)
            {
                return null;
            }

        }


        public async Task<OperationResult> Create(CreateEmp createEmp)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                Random r = new Random();
                var Code = r.Next(11111, 99999);

                var Emp = new TEmployee()
                {
                    PersonnelCode = Code,
                    Address = createEmp.address,
                    FirstName = createEmp.Firstname,
                    FatherName = createEmp.FatherName,
                    DateOfEmployment = createEmp.DateOfEmployment,
                    CodeMelli = createEmp.CodeMelli,
                    Place = createEmp.place,
                    Unitid = Convert.ToInt32(createEmp.UnitId),
                    DateOfBirth = createEmp.DateOfBirth,
                    LastName = createEmp.Lastname,
                    Mobile = createEmp.Mobile,
                    MaritalStatus = createEmp.MaritalStatus,
                    Sex = createEmp.Sex,
                    DegreeEducation = createEmp.DegreeEducation,
                };

                if (createEmp.PicFile != null)
                    Emp.Picture = _uploadFile.Upload(createEmp.PicFile);

                if (await _applicationContext.Employee.AnyAsync(c => c.CodeMelli == Emp.CodeMelli))
                    return operationResult.Failed("کد ملی قبلا ثبت شده");

                if (await _applicationContext.Employee.AnyAsync(c => c.PersonnelCode == Emp.PersonnelCode))
                    return operationResult.Failed("لطفا یکبار دیگر تلاش کنید");

                await _applicationContext.AddAsync(Emp);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<CreateEmp> GetForEdit(string Id)
        {

            var _emp = await _applicationContext.Employee.Where(c => c.PersonnelCode == Convert.ToInt32(Id)).Select(c =>
                new CreateEmp()
                {
                    CodePersonnel = c.PersonnelCode.ToString(),
                    CodeMelli = c.CodeMelli,
                    Lastname = c.LastName,
                    FatherName = c.FatherName,
                    Firstname = c.FirstName,
                    place = c.Place,
                    Sex = c.Sex,
                    DegreeEducation = c.DegreeEducation,
                    MaritalStatus = c.MaritalStatus,
                    Mobile = c.Mobile,
                    UnitId = c.Unitid.ToString(),
                    address = c.Address,
                    Pic = c.Picture,
                    DateOfEmployment = c.DateOfEmployment,
                    DateOfBirth = c.DateOfBirth,
                    UnitName = c.TUnit.Name

                }).SingleOrDefaultAsync();


            return _emp;
        }

        public async Task<OperationResult> Edit(CreateEmp createEmp)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _emp = await _applicationContext.Employee.FindAsync(Convert.ToInt32(createEmp.CodePersonnel));

                if (_emp is null)
                    return operationResult.Failed();

                _emp.Address = createEmp.address;
                _emp.CodeMelli = createEmp.CodeMelli;
                _emp.DateOfBirth = createEmp.DateOfBirth;
                _emp.DegreeEducation = createEmp.DegreeEducation;
                _emp.MaritalStatus = createEmp.MaritalStatus;
                _emp.FirstName = createEmp.Firstname;
                _emp.LastName = createEmp.Lastname;
                _emp.DateOfEmployment = createEmp.DateOfEmployment;
                _emp.Place = createEmp.place;
                _emp.Sex = createEmp.Sex;
                _emp.FatherName = createEmp.FatherName;
                _emp.Unitid = Convert.ToInt32(createEmp.UnitId);
                _emp.Mobile = createEmp.Mobile;

                if (createEmp.PicFile != null)
                {
                    _uploadFile.DeleteFile(_emp.Picture);
                    _emp.Picture = _uploadFile.Upload(createEmp.PicFile);
                }

                _applicationContext.Employee.Update(_emp);
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

            var _em = await _applicationContext.Employee.FindAsync(Convert.ToInt32(Id));
            if (_em is null)
                return operationResult.Failed();


            _uploadFile.DeleteFile(_em.Picture);
            _applicationContext.Remove(_em);
            await _applicationContext.SaveChangesAsync();
            return operationResult.Succeeded();


        }

        public async Task<bool> IsExistPersonnelCode(int ID)
        {
            return await _applicationContext.Employee.AnyAsync(c => c.PersonnelCode == Convert.ToInt32(ID));
        }
    }
}
