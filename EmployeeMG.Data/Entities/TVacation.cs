namespace EmployeeMG.Data.Entities
{
    public class TVacation
    {
        public int Id { get; set; }
        public int PersonnelCode { get; set; }
        public string FromDate { get; set; }
        public string inmonth { get; set; }
        public int CountDate { get; set; }
        public int PersonnelCodeSuccessor { get; set; }
    }
}