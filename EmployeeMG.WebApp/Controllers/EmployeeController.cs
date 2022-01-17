using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Employee;
using EmployeeMG.Core.ApplicationContract.Unit;
using EmployeeMG.WebApp.Common;
using EmployeeMG.WebApp.Common.MessageBox;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeMG.WebApp.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeApplication _employeeApplication;
        private readonly IUnitApplication _unitApplication;
        private readonly ImsgBox _msgBox;

        public EmployeeController(IEmployeeApplication employeeApplication, ImsgBox msgBox, IUnitApplication unitApplication)
        {
            _employeeApplication = employeeApplication;
            _msgBox = msgBox;
            _unitApplication = unitApplication;
        }

        public async Task<IActionResult> Index(int PageId = 1, int take = 10,string filterName="",string filtermelli="",string filtercode = "")
        {
            var _emp = await _employeeApplication.GetAll(PageId, take, filterName, filtermelli, filtercode);

            return View(_emp);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int PageId, int take, string filterName, string filtermelli, string filtercode)
        {
            var _emp = await _employeeApplication.GetAll(PageId, take, filterName, filtermelli, filtercode);

            return View("_Employee",_emp);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.unit = new SelectList(await _unitApplication.GetForPersonnel(),"Code","Name");
            return View("_Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmp createEmp)
        {
            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());
            var result = await _employeeApplication.Create(createEmp);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }

        public async Task<IActionResult> Edit(string ID)
        {
            var Emp = await _employeeApplication.GetForEdit(ID);
            if (Emp is null)
                return NotFound();

            ViewBag.unit = new SelectList(await _unitApplication.GetForPersonnel(), "Code", "Name");

            return View("_Edit", Emp);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateEmp createEmp)
        {
            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());
            var result = await _employeeApplication.Edit(createEmp);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }

        public async Task<IActionResult> Show(string ID)
        {
            var _Emp = await _employeeApplication.GetForEdit(ID);
            if (_Emp is null)
                return NotFound();

            return View("_Show", _Emp);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string ID)
        {
            var result = await _employeeApplication.Delete(ID);
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
