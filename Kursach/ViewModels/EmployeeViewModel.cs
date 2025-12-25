using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SecurityCompanyWPF.Models;

namespace SecurityCompanyWPF.ViewModels
{
    public class EmployeeViewModel : ObservableObject
    {
        private Employee _employee;

        public string LastName
        {
            get => _employee.LastName;
            set
            {
                _employee.LastName = value;
                OnPropertyChanged(nameof(LastName));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string FirstName
        {
            get => _employee.FirstName;
            set
            {
                _employee.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string Position
        {
            get => _employee.Position;
            set
            {
                _employee.Position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public decimal BaseSalary
        {
            get => _employee.BaseSalary;
            set
            {
                _employee.BaseSalary = value;
                OnPropertyChanged(nameof(BaseSalary));
                OnPropertyChanged(nameof(TotalSalary));
            }
        }

        public decimal Allowance
        {
            get => _employee.Allowance;
            set
            {
                _employee.Allowance = value;
                OnPropertyChanged(nameof(Allowance));
                OnPropertyChanged(nameof(TotalSalary));
            }
        }

        public bool HasWeapon
        {
            get => _employee.HasWeapon;
            set
            {
                _employee.HasWeapon = value;
                OnPropertyChanged(nameof(HasWeapon));
            }
        }

        public string FullName => _employee.FullName;
        public decimal TotalSalary => _employee.CalculateSalary();

        public Employee Employee => _employee;

        public EmployeeViewModel(Employee employee = null)
        {
            _employee = employee ?? new Employee();
        }
    }
}