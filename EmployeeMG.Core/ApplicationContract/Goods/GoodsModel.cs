using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.Core;

namespace EmployeeMG.Core.ApplicationContract.Goods
{

    public interface IGoodsApplication
    {
        Task<ListGoods> GetAll(int pageId, int take, int filterCode, string filterName);
        Task<OperationResult> Create(int code, string Name, int count);
        Task<GoodsModel> GetForEdit(int Code);
        Task<OperationResult> Edit(GoodsModel goodsModel);
        Task<OperationResult> Delete(int Code);
        Task<ListLandingGoods> GetAllLandingGoods(int pageId, int take, int FilterPersonnelCode, int FilterCodeGoods);
        Task<OperationResult> CreateLendingGoods(int CodePersonnel, string GoodsCode, int Count);
        Task<List<GoodsModel>> GetForAddLengingGoods();
        Task<OperationResult> DeleteLendingGoods(int ID);
        Task<LendingGoodsModel> GetForEditLendingGoods(int ID);
        Task<OperationResult> EditLendingGoods(LendingGoodsModel goodsModel);
        Task<string> CheckInventory(string CodeGoods);
    }

    public class GoodsModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int  Count { get; set; }

    }

    public class ListGoods:BasePaging
    {
        public List<GoodsModel> GoodsModels  { get; set; }
        public string FilterName { get; set; }

        public int FilterCode { get; set; }
    }

    public class LendingGoodsModel
    {
        public int  ID { get; set; }
        public int GoodsCode { get; set; }
        public string GoodsName { get; set; }
        public int  PersonnelCode { get; set; }
        public int Count { get; set; }
        
    }

    public class ListLandingGoods:BasePaging
    {
        public List<LendingGoodsModel> LendingGoodsModels { get; set; }
        public int FilterPersonnelCode { get; set; }
        public int FilterCodeGoods { get; set; }

    }

}
