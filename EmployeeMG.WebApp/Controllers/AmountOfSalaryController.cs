using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.AmountOfSalary;
using EmployeeMG.Core.ApplicationContract.Unit;
using EmployeeMG.WebApp.Common;
using EmployeeMG.WebApp.Common.MessageBox;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeMG.WebApp.Controllers
{
    public class AmountOfSalaryController : Controller
    {
        private readonly IAmountOfSalaryApplication _amountOfSalaryApplication;
        private readonly IUnitApplication _unitApplication;
        private readonly ImsgBox _msgBox;

        public AmountOfSalaryController(IAmountOfSalaryApplication amountOfSalaryApplication,
            IUnitApplication unitApplication, ImsgBox msgBox)
        {
            _amountOfSalaryApplication = amountOfSalaryApplication;
            _unitApplication = unitApplication;
            _msgBox = msgBox;
        }

        public async Task<IActionResult> Index(int pageid = 1, int take = 10, string fun = "")
        {
            var _Data = await _amountOfSalaryApplication.GetAll(pageid, take, fun);
            ViewBag.UnitName = new SelectList(await _unitApplication.GetForPersonnel(), "Name", "Name");
            return View(_Data);
        }


        [HttpPost]
        public async Task<IActionResult> Search(int pageid, int take, string fun)
        {
            var _Data = await _amountOfSalaryApplication.GetAll(pageid, take, fun);
            ViewBag.UnitName = new SelectList(await _unitApplication.GetForPersonnel(), "Name", "Name");
            return View("_AmountOfSalary", _Data);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.UnitName = new SelectList(await _unitApplication.GetForPersonnel(), "Code", "Name");
            return View("_Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAmountOfSalary amountOfSalary)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.UnitName = new SelectList(await _unitApplication.GetForPersonnel(), "Code", "Name");
                return _msgBox.FaildMsg(ModelState.GetErrors());
            }

            var result = await _amountOfSalaryApplication.Create(amountOfSalary);
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
            var _AmountOfSalary = await _amountOfSalaryApplication.GetForEdit(ID);

            if (_AmountOfSalary is null)
                return NotFound();

            ViewBag.UnitName = new SelectList(await _unitApplication.GetForPersonnel(), "Code", "Name");

            return View("_Edit", _AmountOfSalary);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAmountOfSalary amountOfSalary)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UnitName = new SelectList(await _unitApplication.GetForPersonnel(), "Code", "Name");
                return _msgBox.FaildMsg(ModelState.GetErrors());
            }

            var result = await _amountOfSalaryApplication.Edit(amountOfSalary);
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
            var result = await _amountOfSalaryApplication.Delete(ID);
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
            var _AmountOfSalary = await _amountOfSalaryApplication.GetForEdit(ID);

            if (_AmountOfSalary is null)
                return NotFound();

            return View("_Show", _AmountOfSalary);
        }
    }
}
