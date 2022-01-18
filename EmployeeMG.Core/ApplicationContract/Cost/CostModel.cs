using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.Core;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EmployeeMG.Core.ApplicationContract.Cost
{


    public interface ICostApplication
    {
        Task<ListCost> GetAll(int pageId, int take, string FilterCostDate, string FilterSort);
        Task<OperationResult> Create(string forthe, string dateCost, long amount);
        Task<CostModel> GetForEdit(int Id);
        Task<OperationResult> Edit(CostModel costModel);
        Task<OperationResult> Delete(int ID);
    }
   public class CostModel
    {
        public long Id { get; set; }

        [Required]
        [DisplayName("بابت")]
        public string Forthe { get; set; }

        [Required]
        [DisplayName("در تاریخ")]
        public string CostDate { get; set; }

        [Required]
        [DisplayName("قیمت")]
        public long Amount { get; set; }
    }

   public class ListCost : BasePaging
   {
       public List<CostModel> CostModels{ get; set; }
       public string FilterDateCost { get; set; }
       public string FilterSort { get; set; }
   }

}
