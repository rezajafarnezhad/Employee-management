namespace EmployeeMG.Data.Entities
{
    public class TAmountOfSalary
    {
        public int  Id { get; set; }
        public int Salary { get; set; }
        public int RightHousing { get; set; }
        public int RightChild { get; set; }
        public int Insurance { get; set; }
        public int HourOfOverTime { get; set; }
        public int UnitId { get; set; }
        public TUnit TUnit { get; set; }
    }
}