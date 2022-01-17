using System.Collections.Generic;

namespace EmployeeMG.Data.Entities
{
    public class TUnit
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public ICollection<TEmployee> TEmployees { get; set; }
        public TAmountOfSalary TAmountOfSalary { get; set; }
    }
}