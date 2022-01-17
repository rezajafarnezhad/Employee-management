using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.Core;

namespace EmployeeMG.Core.ApplicationContract.BankAccount
{


    public interface IBankAccountApplication
    {
        Task<ListBankAccount> GetAll(int pageId, int take, int FilterCodepersonnel,
            string FilterCodeShaba);

        Task<BankAccountModel> GetForEdit(string id);
        Task<OperationResult> Create(int PersonnelCode, string CodeShaba);
        Task<OperationResult> Delete(string Id);
        Task<OperationResult> Edit(BankAccountModel accountModel);
    }


    public class BankAccountModel
    {
        public string Id { get; set; }
        public int PersonnelCode { get; set; }
        public string Shaba { get; set; }
    }

    public class ListBankAccount:BasePaging
    {
        public List<BankAccountModel> BankAccountModels { get; set; }
        public int FilterCodePersonnel { get; set; }
        public string FilterCodeshaba { get; set; }

    }
}
