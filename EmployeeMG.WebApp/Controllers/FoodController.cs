using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Food;
using EmployeeMG.WebApp.Common.MessageBox;

namespace EmployeeMG.WebApp.Controllers
{
    public class FoodController : Controller
    {
        private readonly IFoodApplication _foodApplication;
        private readonly ImsgBox _msgBox;

        public FoodController(IFoodApplication foodApplication, ImsgBox msgBox)
        {
            _foodApplication = foodApplication;
            _msgBox = msgBox;
        }

        public async Task<IActionResult> Index(int pageId = 1, int take = 10, int ffc = 0, string ffn = "")
        {

            var _foods = await _foodApplication.GetAll(pageId, take, ffc, ffn);
            return View(_foods);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int pageId, int take, int ffc, string ffn)
        {

            var _foods = await _foodApplication.GetAll(pageId, take = 10, ffc, ffn);
            return View("_food", _foods);
        }


        [HttpPost]
        public async Task<IActionResult> Create(string Name)
        {
            if (string.IsNullOrWhiteSpace(Name))
                return _msgBox.FaildMsg("نام غذا را وارد کنید");

            var result = await _foodApplication.Create(Name);
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

            var result = await _foodApplication.Delete(ID);
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
        public async Task<IActionResult> Av(int number1, int number2, int number3)
        {
            List<int> listnum = new List<int>()
            {
                number1,
                 number2,
                 number3
            };

            var avreage = Convert.ToInt32(listnum.Average());
            var m = avreage;
            return new JsResult(m.ToString());
        }
    }
}
