namespace SecurityCompanyWPF.Models
{
    public abstract class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public abstract decimal CalculateSalary();

        public virtual void UpdatePersonInfo(Person updatedPerson)
        {
            LastName = updatedPerson.LastName;
            FirstName = updatedPerson.FirstName;
            MiddleName = updatedPerson.MiddleName;
            Address = updatedPerson.Address;
            Phone = updatedPerson.Phone;
        }

        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();
    }
}