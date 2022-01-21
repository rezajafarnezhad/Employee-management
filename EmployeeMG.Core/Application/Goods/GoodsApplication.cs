using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.ApplicationContract.Goods;
using EmployeeMG.Core.Core;
using EmployeeMG.Data.Context;
using EmployeeMG.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EmployeeMG.Core.Application.Goods
{
    public class GoodsApplication : IGoodsApplication
    {

        private readonly ApplicationContext _applicationContext;

        public GoodsApplication(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<ListGoods> GetAll(int pageId, int take, int filterCode, string filterName)
        {
            try
            {
                var result = _applicationContext.Goods.Select(c => new GoodsModel()
                {
                    Code = c.Code,
                    Count = c.Count,
                    Name = c.Name,
                });

                if (!string.IsNullOrWhiteSpace(filterName))
                    result = result.Where(c => c.Name == filterName);

                if (filterCode != 0)
                    result = result.Where(c => c.Code == filterCode);

                var skip = (pageId - 1) * take;

                var model = new ListGoods()
                {
                    FilterName = filterName,
                    FilterCode = filterCode,
                    GoodsModels = await result.OrderByDescending(c => c.Code).Skip(skip).Take(take).ToListAsync(),
                };

                model.GenaratPaging(result, pageId, take);
                return model;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> Create(int code, string Name, int count)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _Goods = new TGoods()
                {
                    Code = code,
                    Name = Name,
                    Count = count,
                };

                if (await _applicationContext.Goods.AnyAsync(c => c.Code == _Goods.Code))
                    return operationResult.Failed("این کد قبلا ثبت شده");

                await _applicationContext.AddAsync(_Goods);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<GoodsModel> GetForEdit(int Code)
        {
            try
            {
                var _goods = await _applicationContext.Goods.Where(c => c.Code == Code).Select(c => new GoodsModel()
                {
                    Name = c.Name,
                    Count = c.Count,
                    Code = c.Code
                }).SingleOrDefaultAsync();

                return _goods;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<OperationResult> Edit(GoodsModel goodsModel)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _goods = await _applicationContext.Goods.FindAsync(goodsModel.Code);

                _goods.Name = goodsModel.Name;
                _goods.Count = goodsModel.Count;

                _applicationContext.Update(_goods);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();


            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<OperationResult> Delete(int Code)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _goods = await _applicationContext.Goods.FindAsync(Code);

                _applicationContext.Remove(_goods);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }


        public async Task<ListLandingGoods> GetAllLandingGoods(int pageId, int take, int FilterPersonnelCode, int FilterCodeGoods)
        {
            try
            {
                var result = _applicationContext.LendingGoods.Select(c => new LendingGoodsModel()
                {
                    ID = c.Id,
                    PersonnelCode = c.PersonnelCode,
                    Count = c.Count,
                    GoodsCode = c.GoodsCode,
                    GoodsName = _applicationContext.Goods.Where(a => a.Code == c.GoodsCode).Select(a => a.Name).SingleOrDefault(),
                });


                if (FilterPersonnelCode != 0)
                    result = result.Where(c => c.PersonnelCode == FilterPersonnelCode);

                if (FilterCodeGoods != 0)
                    result = result.Where(c => c.GoodsCode == FilterCodeGoods);

                var skip = (pageId - 1) * take;

                var model = new ListLandingGoods()
                {
                    FilterCodeGoods = FilterCodeGoods,
                    FilterPersonnelCode = FilterPersonnelCode,
                    LendingGoodsModels = await result.OrderByDescending(c => c.ID).Skip(skip).Take(take).ToListAsync(),
                };

                model.GenaratPaging(result, pageId, take);
                return model;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OperationResult> CreateLendingGoods(int CodePersonnel, string GoodsCode, int Count)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                Random r = new Random();
                var _Langing = new TLendingGoods()
                {
                    Id = r.Next(111111, 999999),
                    PersonnelCode = CodePersonnel,
                    Count = Count,
                    GoodsCode =Convert.ToInt32(GoodsCode),
                };

                if (!await _applicationContext.Employee.AnyAsync(c => c.PersonnelCode == _Langing.PersonnelCode))
                    return operationResult.Failed("این کد پرسنلی وجود ندارد");

                if (await _applicationContext.Goods.AnyAsync(c => c.Code == _Langing.GoodsCode && c.Count < _Langing.Count))
                    return operationResult.Failed("تعداد در خواستی از این کالا وجود ندارد");

                await _applicationContext.LendingGoods.AddAsync(_Langing);


                var _goods = await _applicationContext.Goods.Where(c => c.Code == _Langing.GoodsCode).SingleOrDefaultAsync();
                _goods.Count -= _Langing.Count;
                _applicationContext.Goods.Update(_goods);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<List<GoodsModel>> GetForAddLengingGoods()
        {
            return await _applicationContext.Goods.Select(c => new GoodsModel()
            {
                Code = c.Code,
                Name = c.Name
            }).ToListAsync();

        }

        public async Task<OperationResult> DeleteLendingGoods(int ID)
        {
            OperationResult operationResult = new OperationResult();

            var _GoodsLending = await _applicationContext.LendingGoods.FindAsync(ID);

            try
            {
                if (_GoodsLending is null)
                    return operationResult.Failed();

                var _goods = await _applicationContext.Goods.Where(c => c.Code == _GoodsLending.GoodsCode).SingleOrDefaultAsync();

                _goods.Count += _GoodsLending.Count;

                _applicationContext.Goods.Update(_goods);
                _applicationContext.LendingGoods.Remove(_GoodsLending);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();

            }
            catch (Exception)
            {
                return operationResult.Failed();
            }
        }

        public async Task<string> CheckInventory(string CodeGoods)
        {
            var _count = await _applicationContext.Goods.Where(c => c.Code == Convert.ToInt32(CodeGoods)).Select(c => c.Count).SingleOrDefaultAsync();

            return _count.ToString();

        }



        public async Task<LendingGoodsModel> GetForEditLendingGoods(int ID)
        {
            var _goods = await _applicationContext.LendingGoods.Where(c => c.Id == ID)
                .Select(c => new LendingGoodsModel()
                {

                    Count = c.Count,
                    PersonnelCode = c.PersonnelCode,
                    GoodsCode = c.GoodsCode,
                    ID = ID
                }).SingleOrDefaultAsync();

            return _goods;
        }

        public async Task<OperationResult> EditLendingGoods(LendingGoodsModel goodsModel)
        {
            OperationResult operationResult = new OperationResult();

            try
            {
                var _goodslen = await _applicationContext.LendingGoods.FindAsync(goodsModel.ID);
            
                if (_goodslen is null)
                    return operationResult.Failed();

                if (!await _applicationContext.Employee.AnyAsync(c => c.PersonnelCode == goodsModel.PersonnelCode))
                    return operationResult.Failed("این کد پرسنلی وجود ندارد");

                

                _goodslen.PersonnelCode = goodsModel.PersonnelCode;
                _goodslen.GoodsCode = goodsModel.GoodsCode;
                var _goods = await _applicationContext.Goods.Where(c => c.Code == goodsModel.GoodsCode).SingleOrDefaultAsync();
                
                
                
                
                if (_goodslen.Count > goodsModel.Count)
                {
                    _goodslen.Count -= goodsModel.Count;
                    _goods.Count += goodsModel.Count;
                }
                else if (_goodslen.Count < goodsModel.Count)
                {
                    _goodslen.Count = goodsModel.Count;
                    _goods.Count -= goodsModel.Count;
                    if (_goods.Count < 0)
                        _goods.Count = 0;
                    if (await _applicationContext.Goods.AnyAsync(c => c.Code == _goodslen.GoodsCode && c.Count+_goods.Count < _goodslen.Count))
                        return operationResult.Failed("موجودی کافی نی");
                }

                


                _applicationContext.Goods.Update(_goods); 
                _applicationContext.LendingGoods.Update(_goodslen);
                await _applicationContext.SaveChangesAsync();
                return operationResult.Succeeded();
            }
            catch (Exception)
            {
                return operationResult.Failed();

            }
        }

    }
}
