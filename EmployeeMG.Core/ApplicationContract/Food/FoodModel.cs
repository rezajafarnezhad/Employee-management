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
        Task<ListNUTritionCard> GetAllCard(int pageId, int take, int fpc);
        Task<OperationResult> CreateNUTCard(int codePersonnley, int Balabce);
        Task<NUTritionCardModel> GeForbalanceAdd(int ID);
        Task<OperationResult> balanceAdd(NUTritionCardModel nuTritionCardModel);
        Task<OperationResult> DeleteNUTritionCard(int ID); 
        Task<ListFoodOffered> GetAllFoodOffered(int pageId, int take, string FilterDateOffered, string FilterMeal);
        Task<OperationResult> CreateOffered(FoodOfferedModel foodOfferedModel);
        Task<FoodOfferedModel> GetForEditFoodOffered(int ID);
        Task<List<FoodModel>> GetForCreateOffered();
        Task<string> ShowDay(string date);
        Task<OperationResult> Edit(FoodOfferedModel foodOfferedModel);
        Task<OperationResult> DeleteFoodOffered(int ID);
        Task<OperationResult> Reservefood(int Codep, int ID);
        Task<List<int>> ReservationFoodEmployeeShow(int ID);
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

    public class NUTritionCardModel
    {
        public int Id { get; set; }
        public int CodePersonnel { get; set; }
        public int Balance { get; set; }

    }

    public class ListNUTritionCard:BasePaging
    {
        public List<NUTritionCardModel> NuTritionCardModels { get; set; }
        public int CodePersonnel { get; set; }
    }


    public class FoodOfferedModel
    {
        public int ID { get; set; }
        public string DateOffered { get; set; }
        public string Day { get; set; }
        public string Meal { get; set; }
        public string FoodName { get; set; }

    }

    public class ListFoodOffered : BasePaging
    {
        public List<FoodOfferedModel> FoodOfferedModels { get; set; }
        public string FilterDateOffered { get; set; }
        public string FilterMeal { get; set; }
    }

}
