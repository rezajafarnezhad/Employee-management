using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.Core;

namespace EmployeeMG.Core.ApplicationContract.AmountOfSalary
{

    public interface 
        IAmountOfSalaryApplication
    {
        Task<ListAmountOfSalary> GetAll(int pageId, int take, string FilterUnitName);
        Task<OperationResult> Create(CreateAmountOfSalary amountOfSalary);
        Task<OperationResult> Delete(string Id);
        Task<EditAmountOfSalary> GetForEdit(string Id);
        Task<OperationResult> Edit(EditAmountOfSalary amountOfSalary);
    }

    public class AmountOfSalaryModel
    {
        public string Id { get; set; }
        public int Salary { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
    }

    public class CreateAmountOfSalary
    {
        [Required]
        [DisplayName("واحد")]
        public string UnitId { get; set; }

        [Required]
        [DisplayName("میزان حقوق")]
        public int Salary { get; set; }

        [Required]
        [DisplayName("حق مسکن")]
        public int RightHousing { get; set; }

        [Required]
        [DisplayName("حق اولاد")]
        public int RightChild { get; set; }
        [Required]
        [DisplayName("بیمه")]
        public int Insurance { get; set; }

        [Required]
        [DisplayName("هر ساعت اضافه کاری")]
        public int HourOfOverTime { get; set; }
    }

    public class EditAmountOfSalary : CreateAmountOfSalary
    {
        public string Id { get; set; }
        public string UnitName { get; set; }
    }


    public class ListAmountOfSalary:BasePaging
    {
        public List<AmountOfSalaryModel> AmountOfSalaryModels  { get; set; }
        public string FilterUnitName { get; set; }

    }

}
