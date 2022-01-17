using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.Core;

namespace EmployeeMG.Core.ApplicationContract.Vacation
{

    public interface IVacationApplication
    {
        Task<ListVacation> GetAll(int pageId, int take, int FilterCodePersonnel, string FilterFromDate);
        Task<OperationResult> CreateVacation(CreateVacation vacation);
        Task<OperationResult> Delete(string ID);
    }

    public class VacationModel
    {
        public string Id { get; set; }
        public int PersonnelCode { get; set; }
        public string FromDate { get; set; }
        public int CountDate { get; set; }
    }

    public class CreateVacation
    {
        [Required]
        [DisplayName("کد پرسنلی کارمند جانشین")]
        public int PersonnelCodeSuccessor { get; set; }
        public string Id { get; set; }

        [Required]
        [DisplayName("کد پرسنلی کارمند ")]
        public int PersonnelCode { get; set; }

        [Required]
        [DisplayName("از تاریخ")]
        public string FromDate { get; set; }

        [Required]
        [DisplayName("تعداد روز")]
        //[Range(1, 4, ErrorMessage = "کاربران در ماه مجاز به حداکثر 4 روز مرخصی هستند")]
        //[MaxLength(4,ErrorMessage = "کاربران در ماه مجاز به حداکثر 4 روز مرخصی هستند")]
        public int CountDate { get; set; }
    }

    public class ListVacation:BasePaging
    {
        public List<VacationModel> VacationModels  { get; set; }
        public int FilretCodePersonnel { get; set; }
        public string FilterFromDate { get; set; }
    }
}
