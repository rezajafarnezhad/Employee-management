using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Food;
using EmployeeMG.Core.Core;
using EmployeeMG.WebApp.Common.MessageBox;
using Microsoft.AspNetCore.Mvc.Rendering;

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


        public async Task<IActionResult> IndexnutCard(int pageid=1, int take=20, int fpc=0)
        {
            var _Card = await _foodApplication.GetAllCard(pageid,take, fpc);
            return View("IndexnutCard",_Card);
        }

        [HttpPost]
        public async Task<IActionResult> SearchNUT(int pageid, int take, int fpc)
        {
            var _Card = await _foodApplication.GetAllCard(pageid, take=20, fpc);
            return View("_NUTCard" ,_Card);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNUT(int codePersonnley, int Balabce)
        {
            if (Balabce == 0 || codePersonnley == 0)
                return _msgBox.FaildMsg("اطلاعات وارد شود");

            var result = await _foodApplication.CreateNUTCard(codePersonnley, Balabce);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }


        public async Task<IActionResult> AddBalance(int ID)
        {
            var _card = await _foodApplication.GeForbalanceAdd(ID);
            return View("_AddBalance",_card);
        }

        [HttpPost]
        public async Task<IActionResult> AddBalance(NUTritionCardModel cardModel)
        {
            if (cardModel.Balance == 0)
                return _msgBox.FaildMsg("موجودی وارد شود");

            var result = await _foodApplication.balanceAdd(cardModel);
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
        public async Task<IActionResult> DeleteUNTCard(int ID)
        {
            var result = await _foodApplication.DeleteNUTritionCard(ID);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }


        public async Task<IActionResult> GetAllFoodOffered(int PageId=1, int take =10, string fd="")
        {
            var _foodOffered = await _foodApplication.GetAllFoodOffered(PageId, take, fd);
            return View("IndexFoodOffered", _foodOffered);
        }

        [HttpPost]
        public async Task<IActionResult> SearchFoodOffered(int PageId, int take, string fd)
        {
            var _foodOffered = await _foodApplication.GetAllFoodOffered(PageId, take=10, fd);
            return View("_foodOffered", _foodOffered);
        }

        public async Task<IActionResult> CreateFoodOffered()
        {
            ViewBag.ListFoods = new SelectList(await _foodApplication.GetForCreateOffered(),"Name","Name");
            return View("_CreateFoodOffered");
        }

        [HttpPost]
        public async Task<IActionResult> CreateFoodOffered(FoodOfferedModel foodOfferedModel)
        {
            if (string.IsNullOrWhiteSpace(foodOfferedModel.DateOffered) ||
                string.IsNullOrWhiteSpace(foodOfferedModel.Meal) ||
                string.IsNullOrWhiteSpace(foodOfferedModel.FoodName))
                return _msgBox.FaildMsg("اطلاعات وارد شود");
            var result = await _foodApplication.CreateOffered(foodOfferedModel);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }

        public async Task<IActionResult> ShowDay(string foodOfferedDate)
        {
            if (foodOfferedDate is null)
                foodOfferedDate = DateTime.Now.ToFarsi();

            var _day = await _foodApplication.ShowDay(foodOfferedDate);
            return new JsResult(_day);
        }

        public async Task<IActionResult> EditFoodOffered(int ID)
        {
            var result = await _foodApplication.GetForEditFoodOffered(ID);

            if (result is null)
                return NotFound();

            ViewBag.ListFoods = new SelectList(await _foodApplication.GetForCreateOffered(), "Name", "Name");

            return View("_EditFoodOffered",result);
        }

        [HttpPost]
        public async Task<IActionResult> EditFoodOffered(FoodOfferedModel foodOfferedModel)
        {
            var result = await _foodApplication.Edit(foodOfferedModel);
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
        public async Task<IActionResult> DeleteFoodOffered(int ID)
        {
            var result = await _foodApplication.DeleteFoodOffered(ID);
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
