namespace EmployeeMG.Data.Entities
{
    public class TShift
    {
        public int Id { get; set; }
        public int PersonnelCode { get; set; }
        public string ShiftDate { get; set; }
        public string ShiftDay { get; set; }
        public string ShiftTime { get; set; }
    }
}