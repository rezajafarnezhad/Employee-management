using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Goods;
using EmployeeMG.WebApp.Common.MessageBox;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeMG.WebApp.Controllers
{
    public class GoodsController : Controller
    {
        private readonly IGoodsApplication _goodsApplication;
        private readonly ImsgBox _msgBox;
        public GoodsController(IGoodsApplication goodsApplication, ImsgBox msgBox)
        {
            _goodsApplication = goodsApplication;
            _msgBox = msgBox;
        }

        public async Task<IActionResult> Index(int PageId = 1, int take = 10, int fc = 0, string fn = "")
        {
            var _Goods = await _goodsApplication.GetAll(PageId, take, fc, fn);
            return View("Index", _Goods);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int PageId, int take, int fc, string fn)
        {
            var _Goods = await _goodsApplication.GetAll(PageId, take = 10, fc, fn);
            return View("_Goods", _Goods);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int Code, string Name, int Count)
        {
            if (string.IsNullOrWhiteSpace(Name) || Code == 0 || Count == 0)
                return _msgBox.FaildMsg("اطلاعات وارد شود");

            var result = await _goodsApplication.Create(Code, Name, Count);
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
            var _goods = await _goodsApplication.GetForEdit(ID);
            if (_goods is null)
                return NotFound();

            return View("_Edit", _goods);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(GoodsModel goodsModel)
        {
            if (string.IsNullOrWhiteSpace(goodsModel.Name) || goodsModel.Count == 0 || goodsModel.Code == 0)
                return _msgBox.FaildMsg("اطلاعات وارد شود");

            var result = await _goodsApplication.Edit(goodsModel);
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
            var result = await _goodsApplication.Delete(ID);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }

        }


        public async Task<IActionResult> GetAllLengingGoods(int PageId = 1, int take = 10, int fcp = 0, int fcg = 0)
        {
            var _LengingGoods = await _goodsApplication.GetAllLandingGoods(PageId, take, fcp, fcg);

            ViewBag.GoodsName = new SelectList(await _goodsApplication.GetForAddLengingGoods(), "Code", "Name");

            return View("IndexLending", _LengingGoods);
        }


        [HttpPost]
        public async Task<IActionResult> SearchLendingGoods(int PageId, int take, int fcp, int fcg)
        {
            var _LengingGoods = await _goodsApplication.GetAllLandingGoods(PageId, take, fcp, fcg);
            return View("_GoodsLenging", _LengingGoods);
        }


        [HttpPost]
        public async Task<IActionResult> CreateLending(int Codep, string CodeGoods, int Count)
        {

            if (Codep == 0 || string.IsNullOrWhiteSpace(CodeGoods) || Count == 0)
                return _msgBox.FaildMsg("اطلاعات وارد شود");

            var result = await _goodsApplication.CreateLendingGoods(Codep, CodeGoods, Count);
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
        public async Task<IActionResult> DeleteLending(int ID)
        {

            var result = await _goodsApplication.DeleteLendingGoods(ID);
            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }


        public async Task<IActionResult> EditLending(int ID)
        {
            ViewBag.GoodsName = new SelectList(await _goodsApplication.GetForAddLengingGoods(), "Code", "Name");

            var _goodslending = await _goodsApplication.GetForEditLendingGoods(ID);
            if (_goodslending is null)
                return NotFound();

            return View("_EditLendingGoods", _goodslending);
        }

        [HttpPost]
        public async Task<IActionResult> EditLending(LendingGoodsModel goodsModel)
        {
            if (goodsModel.PersonnelCode == 0 || string.IsNullOrWhiteSpace(goodsModel.GoodsCode.ToString()) || goodsModel.Count == 0)
            {
                ViewBag.GoodsName = new SelectList(await _goodsApplication.GetForAddLengingGoods(), "Code", "Name");
                return _msgBox.FaildMsg("اطلاعات وارد شود");

            }

            var result = await _goodsApplication.EditLendingGoods(goodsModel);

            if (result.isSucceeded)
            {
                return _msgBox.SuccessMsg(result.Message, "RefreshTable()");
            }
            else
            {
                return _msgBox.FaildMsg(result.Message);

            }
        }

        public async Task<IActionResult> CheckInventory(string CodeGoods)
        {
            var _CountInInventory = await _goodsApplication.CheckInventory(CodeGoods);
            return new JsResult(_CountInInventory);
        }

    }
}
