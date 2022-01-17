namespace EmployeeMG.Data.Entities
{
    public class TDepositSalary
    {
        public int Id { get; set; }
        public int PersonnelCode { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public long Amount { get; set; }

    }
}