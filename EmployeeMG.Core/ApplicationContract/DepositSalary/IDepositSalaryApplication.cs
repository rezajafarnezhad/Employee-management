using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.Core;

namespace EmployeeMG.Core.ApplicationContract.DepositSalary
{
    public interface IDepositSalaryApplication
    {
        Task<ListDepositSalary> GetAll(int pageId, int take, int filtercodePersonnel,
            string filterMonth);

        Task<OperationResult> Create(string Month);
    }

    public class DepositSalaryModel
    {
        public int Id { get; set; }
        public int PersonnelCode { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public long Amount { get; set; }
    }

    public class ListDepositSalary:BasePaging
    {
        public List<DepositSalaryModel> DepositSalaryModels { get; set; }
        public int FilterCodePersonnel { get; set; }
        public string FilterMonth { get; set; }
    }

}
