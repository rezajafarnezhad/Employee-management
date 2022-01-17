using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.BankAccount;
using EmployeeMG.WebApp.Common.MessageBox;

namespace EmployeeMG.WebApp.Controllers
{
    public class BankAccountController : Controller
    {
        private readonly IBankAccountApplication _bankAccountApplication;
        private readonly ImsgBox _msgBox;

        public BankAccountController(IBankAccountApplication bankAccountApplication, ImsgBox msgBox)
        {
            _bankAccountApplication = bankAccountApplication;
            _msgBox = msgBox;
        }

        public async Task<IActionResult> Index(int PageId = 1 , int take =10 , int fc =0,string fs="")
        {
            var _bank = await _bankAccountApplication.GetAll(PageId, take, fc, fs);

            return View(_bank);
        }


        [HttpPost]

        public async Task<IActionResult> Search(int PageId, int take, int fc, string fs)
        {
            var _bank = await _bankAccountApplication.GetAll(PageId, take, fc, fs);

            return View("_bankAccount",_bank);
        }


        [HttpPost]
        public async Task<IActionResult> Create(int Codep , string Codesh)
        {
            if (Codep==0 || string.IsNullOrWhiteSpace(Codesh))
                return _msgBox.FaildMsg("اطلاعات وارد شود");
            var result = await _bankAccountApplication.Create(Codep,Codesh);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);
            }
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var _bankacc = await _bankAccountApplication.GetForEdit(Id);

            if (_bankacc is null)
                return NotFound();

            return View("_Edit",_bankacc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BankAccountModel accountModel)
        {
            if (string.IsNullOrWhiteSpace(accountModel.Shaba) || accountModel.PersonnelCode==0)
                return _msgBox.FaildMsg("اطلاعات وارد شود");

            var result = await _bankAccountApplication.Edit(accountModel);
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
        public async Task<IActionResult> Delete(string Id)
        {
            var result = await _bankAccountApplication.Delete(Id);
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
