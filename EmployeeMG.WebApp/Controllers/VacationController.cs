using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Vacation;
using EmployeeMG.WebApp.Common;
using EmployeeMG.WebApp.Common.MessageBox;

namespace EmployeeMG.WebApp.Controllers
{
    public class VacationController : Controller
    {
        private readonly IVacationApplication _vacationApplication;
        private readonly ImsgBox _msgBox;

        public VacationController(IVacationApplication vacationApplication, ImsgBox msgBox)
        {
            _vacationApplication = vacationApplication;
            _msgBox = msgBox;
        }

        public async Task<IActionResult> Index(int PageId = 1,int take=10,int pc =0,string fd="")
        {
            var _vacation = await _vacationApplication.GetAll(PageId, take, pc, fd);
            return View(_vacation);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int PageId, int take,int pc, string fd)
        {
            var _vacation = await _vacationApplication.GetAll(PageId, take, pc, fd);
            return View("_Vacation",_vacation);
        }


        public async Task<IActionResult> Create()
        {
            return View("_create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVacation vacation)
        {
            if (!ModelState.IsValid)
                return _msgBox.FaildMsg(ModelState.GetErrors());

            if(vacation.CountDate>=4 || vacation.CountDate<=0)
                return _msgBox.FaildMsg("کاربران در ماه مجاز به حداکثر 4 روز مرخصی هستند");

            var result = await _vacationApplication.CreateVacation(vacation);
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
        public async Task<IActionResult> Delete(String ID)
        {
            var result = await _vacationApplication.Delete(ID);
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
