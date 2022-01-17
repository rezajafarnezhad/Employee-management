using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Company;
using EmployeeMG.Core.Core;
using EmployeeMG.WebApp.Common;
using EmployeeMG.WebApp.Common.MessageBox;

namespace EmployeeMG.WebApp.Controllers
{
    public class CompanyController : Controller
    {

        private readonly ICompanyApplication _companyApplication;
        private readonly ImsgBox _msgBox;
        public CompanyController(ICompanyApplication companyApplication, ImsgBox msgBox)
        {
            _companyApplication = companyApplication;
            _msgBox = msgBox;
        }

        public async Task<IActionResult> Index()
        {
            var _company = await _companyApplication.ListCompany();
            return View(_company);
        }


        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Create company)
        {

            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());

            var result = await _companyApplication.Create(company);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var _company = await _companyApplication.GetCompany(Id);
            return View(_company);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Edit company)
        {

            if(!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());


            var result = await _companyApplication.Edit(company);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string ID)
        {

            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());


            var result = await _companyApplication.Delete(ID);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }

    }
}
