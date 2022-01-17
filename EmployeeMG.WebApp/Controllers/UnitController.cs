using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Unit;
using EmployeeMG.WebApp.Common.MessageBox;
using Newtonsoft.Json.Serialization;

namespace EmployeeMG.WebApp.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitApplication _unitApplication;
        private readonly ImsgBox _msgBox;
        public UnitController(IUnitApplication unitApplication, ImsgBox msgBox)
        {
            _unitApplication = unitApplication;
            _msgBox = msgBox;
        }

        public async Task<IActionResult> Index(int PageId = 1, int take = 10, string filter = "")
        {
            var _Unit = await _unitApplication.GetAll(PageId, take, filter);
            return View(_Unit);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int PageId, int take, string filter)
        {
            var _Unit = await _unitApplication.GetAll(PageId, take, filter);
            return View("_Unit", _Unit);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return _msgBox.FaildMsg("نام واحد وارد شود");

            var result = await _unitApplication.Create(Name);
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
            var _unit = await _unitApplication.GetForEdit(ID);
            if (_unit is null)
                return NotFound();

            return View("_Edit", _unit);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(UnitModel unitModel)
        {
            if (string.IsNullOrWhiteSpace(unitModel.Name))
                return _msgBox.FaildMsg("نام واحد وارد شود");

            var result = await _unitApplication.Edit(unitModel);
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
            var result = await _unitApplication.Delete(ID);
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
