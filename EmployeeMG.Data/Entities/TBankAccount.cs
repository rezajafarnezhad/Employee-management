namespace EmployeeMG.Data.Entities
{
    public class TBankAccount
    {
        public int Id { get; set; }
        public string Shaba { get; set; }
        public int PersonnelCode { get; set; }
        public TEmployee Employee { get; set; }
    }
}