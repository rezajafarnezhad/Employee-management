using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMG.Core.Core;

namespace EmployeeMG.Core.ApplicationContract.Unit
{


    public interface IUnitApplication
    {
        Task<ListUnit> GetAll(int pageId,int take,string filter);
        Task<OperationResult> Create(string Name);
        Task<OperationResult> Delete(string id);
        Task<UnitModel> GetForEdit(string Id);
        Task<OperationResult> Edit(UnitModel unitModel);
        Task<List<UnitModel>> GetForPersonnel();
    }

    public class UnitModel
    {
        public string Code { get; set; }
        public string Name { get; set; }

    }

    public class ListUnit : BasePaging
    {
        public List<UnitModel> UnitModels { get; set; }
        public string Filter { get; set; }
    }

}
