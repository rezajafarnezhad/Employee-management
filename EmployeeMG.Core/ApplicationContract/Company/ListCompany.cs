using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.Core;
using Microsoft.AspNetCore.Http;

namespace EmployeeMG.Core.ApplicationContract.Company
{


    public interface ICompanyApplication
    {
        Task<List<ListCompany>> ListCompany();
        Task<OperationResult> Create(Create company);
        Task<bool> CompanyIsExists();
        Task<Edit> GetCompany(string Id);
        Task<OperationResult> Edit(Edit edit);
        Task<OperationResult> Delete(string Id);
    }


    public class ListCompany
    {
        public string RegistrationCode { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
    }



    public class Create
    {
        [Required]
        [DisplayName("شماره ثبت")]
        public string RegistrationCode { get; set; }

        [Required]
        [DisplayName("نام")]
        public string Name { get; set; }

        [Required]
        [DisplayName("تلفن")]
        public string Phone { get; set; }

        [Required]
        [DisplayName("آدرس")]
        public string Address { get; set; }


        [Required]
        [DisplayName("وب سایت")]
        public string WebSite { get; set; }
        public string Logo { get; set; }


        public IFormFile FileLogo { get; set; }

    }

    public class Edit:Create
    {
        public string RegistrationCode0 { get; set; }

    }
}
