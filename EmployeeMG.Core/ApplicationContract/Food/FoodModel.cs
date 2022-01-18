using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.Core;

namespace EmployeeMG.Core.ApplicationContract.Food
{

    public interface IFoodApplication
    {
        Task<ListFood> GetAll(int pageId, int take, int ffc, string ffn);
        Task<OperationResult> Create(string foodName);
        Task<OperationResult> Delete(int ID);
    }

    public class FoodModel
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }


    public class ListFood:BasePaging
    {
        public List<FoodModel> FoodModels { get; set; }
        public int FilterFoodCode { get; set; }
        public string FilterFoodName { get; set; }

    }
}
