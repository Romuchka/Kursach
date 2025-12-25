using System;

namespace SecurityCompanyWPF.Models
{
    public class Employee : Person
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Position { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Allowance { get; set; }
        public bool HasWeapon { get; set; }
        public string CertificateNumber { get; set; }
        public string LicenseNumber { get; set; }
        public string INN { get; set; }
        public string PFRNumber { get; set; }
        public bool IsActive { get; set; } = true;

        public override decimal CalculateSalary()
        {
            return BaseSalary + Allowance;
        }

        public void UpdateEmployee(Employee updatedEmployee)
        {
            base.UpdatePersonInfo(updatedEmployee);
            Position = updatedEmployee.Position;
            BaseSalary = updatedEmployee.BaseSalary;
            Allowance = updatedEmployee.Allowance;
            HasWeapon = updatedEmployee.HasWeapon;
            CertificateNumber = updatedEmployee.CertificateNumber;
            LicenseNumber = updatedEmployee.LicenseNumber;
            INN = updatedEmployee.INN;
            PFRNumber = updatedEmployee.PFRNumber;
            IsActive = updatedEmployee.IsActive;
        }

        public string ShortInfo => $"{FullName}, {Position}, ЗП: {CalculateSalary():C}";
    }
}