namespace EmployeeMG.Data.Entities
{
    public class TEmployee
    {
        public int PersonnelCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Sex { get; set; }
        public string CodeMelli { get; set; }
        public string FatherName { get; set; }
        public string DateOfBirth { get; set; }
        public string DateOfEmployment { get; set; }
        public string DegreeEducation { get; set; }
        public string MaritalStatus { get; set; }
        public string Mobile { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }
        public int Unitid { get; set; }
        public TUnit TUnit { get; set; }
        public TBankAccount BankAccount { get; set; }
        public TNUTritionCard TritionCard { get; set; }

    }
}