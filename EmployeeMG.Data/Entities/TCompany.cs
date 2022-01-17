using System;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmployeeMG.Data.Entities
{
    public class TCompany
    {
        public string RegistrationNumber { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string WebSite { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }


        public void Edit(string _RegistrationNumber, string _CompanyName, string _Phone, string _WebSite, string _Address,
            string _Logo)
        {
            RegistrationNumber = _RegistrationNumber;
            Logo = _Logo;
            Phone = _Phone;
            CompanyName = _CompanyName;
            Address = _Address;
            WebSite = _WebSite;
        }
    }
}
