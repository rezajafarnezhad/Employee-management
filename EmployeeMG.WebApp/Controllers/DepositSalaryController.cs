using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.DepositSalary;
using EmployeeMG.WebApp.Common.MessageBox;

namespace EmployeeMG.WebApp.Controllers
{
    public class DepositSalaryController : Controller
    {

        private readonly IDepositSalaryApplication _depositSalaryApplication;
        private readonly ImsgBox _msgBox;

        public DepositSalaryController(IDepositSalaryApplication depositSalaryApplication, ImsgBox msgBox)
        {
            _depositSalaryApplication = depositSalaryApplication;
            _msgBox = msgBox;
        }

        public async Task<IActionResult> Index(int PageId = 1, int take = 20, int fpc=0, string mo = "")
        {
            var _data = await _depositSalaryApplication.GetAll(PageId, take, fpc, mo);
            return View(_data);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int PageId, int take, int fpc, string mo)
        {
            var _data = await _depositSalaryApplication.GetAll(PageId, take, fpc, mo);
            return View("_Deposit", _data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Month)
        {
            if (string.IsNullOrWhiteSpace(Month))
                return _msgBox.FaildMsg("ماه واریز انتخاب شود");


            var result = await _depositSalaryApplication.Create(Month);
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
