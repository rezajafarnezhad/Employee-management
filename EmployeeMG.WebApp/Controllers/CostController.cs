using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Cost;
using EmployeeMG.WebApp.Common;
using EmployeeMG.WebApp.Common.MessageBox;

namespace EmployeeMG.WebApp.Controllers
{
    public class CostController : Controller
    {
        private readonly ICostApplication _costApplication;
        private readonly ImsgBox _msgBox;

        public CostController(ImsgBox msgBox, ICostApplication costApplication)
        {
            _msgBox = msgBox;
            _costApplication = costApplication;
        }

        public async Task<IActionResult> Index(int PageId = 1 , int take=10 , string fdate="", string fsort = "")
        {
            var _cost = await _costApplication.GetAll(PageId, take, fdate, fsort);
            return View(_cost);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int PageId, int take, string fdate, string fsort)
        {
            var _cost = await _costApplication.GetAll(PageId, take, fdate, fsort);
            return View("_Cost",_cost);
        }

        public async Task<IActionResult> Create()
        {
            return View("_Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string forthe,string CostDate, long Amount)
        {
            if (string.IsNullOrWhiteSpace(forthe) || Amount == 0)
                return _msgBox.FaildMsg("اطلاعات وارد شود");
            var result = await _costApplication.Create(forthe,CostDate,Amount);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }

        }

        public async Task<IActionResult> Edit(int ID)
        {
            var _cost = await _costApplication.GetForEdit(ID);

            if (_cost is null)
                return NotFound();

            return View("_Edit", _cost);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CostModel costModel)
        {
            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());

            var result = await _costApplication.Edit(costModel);
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
        public async Task<IActionResult> Delete(int ID)
        {

            var result = await _costApplication.Delete(ID);
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
