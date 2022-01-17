namespace EmployeeMG.Data.Entities
{
    public class TNUTritionCard
    {
        public int Id { get; set; }
        public int Balance { get; set; }

        public int PersonnelCode { get; set; }
        public TEmployee TEmployee { get; set; }
    }
}