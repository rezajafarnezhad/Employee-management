using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.Core;
using Microsoft.AspNetCore.Http;

namespace EmployeeMG.Core.ApplicationContract.Employee
{
    public interface IEmployeeApplication
    {
        Task<ListEmployee> GetAll(int pageId, int take, string filterName, string filterCodeMelli,
            string filterCodePersonnle);

        Task<OperationResult> Create(CreateEmp createEmp);
        Task<CreateEmp> GetForEdit(string Id);
        Task<OperationResult> Edit(CreateEmp createEmp);
        Task<OperationResult> Delete(string Id);
        Task<bool> IsExistPersonnelCode(int ID);
    }


    public class EmployeeModel
    {
        public string CodePersonnel { get; set; }
        public string CodeMelli { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string place { get; set; }
        public string DateOfEmployment { get; set; }
    }

    public class ListEmployee:BasePaging
    {
        public string FilterName { get; set; }
        public string FilterCodeMelli { get; set; }
        public string FilterCodePersonnel { get; set; }
        public List<EmployeeModel> EmployeeModels { get; set; }

    }

    public class CreateEmp
    {
        public string CodePersonnel { get; set; }
        public string UnitName { get; set; }
        public string Pic { get; set; }

        [Required]
        [DisplayName("کد ملی")]
        public string CodeMelli { get; set; }

        [Required]
        [DisplayName("نام")]
        public string Firstname { get; set; }

        [Required]
        [DisplayName("فامیلی")]
        public string Lastname { get; set; }
        [Required]
        [DisplayName("سمت")]
        public string place { get; set; }

        [Required]
        [DisplayName("جنسیت")]

        public string Sex { get; set; }
        [Required]
        [DisplayName("اسم پدر")]
        public string FatherName { get; set; }

        [Required]
        [DisplayName("مدرک")]

        public string DegreeEducation { get; set; }
        [Required]
        [DisplayName("وضعیت تاهل")]

        public string MaritalStatus { get; set; }
        [Required]
        [DisplayName("موبایل")]
        public string Mobile { get; set; }

        [Required]
        [DisplayName("واحد")]

        public string UnitId { get; set; }
        [Required]
        [DisplayName("آدرس")]
        public string address { get; set; }
        public IFormFile PicFile { get; set; }

        [Required]
        [DisplayName("تاریخ تولد وارد شود")]
        public string DateOfBirth { get; set; }
        [Required]
        [DisplayName("تاریخ استخدام وارد شود")]
        public string DateOfEmployment { get; set; }
    }


}
