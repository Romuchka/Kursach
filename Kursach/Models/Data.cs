using System.Collections.Generic;

namespace SecurityCompanyWPF.Models
{
    public class Data
    {
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public List<Client> Clients { get; set; } = new List<Client>();
        public List<SecurityContract> Contracts { get; set; } = new List<SecurityContract>();
        public List<DutySchedule> DutySchedules { get; set; } = new List<DutySchedule>();
        public List<Payment> Payments { get; set; } = new List<Payment>();
        public List<Event> Events { get; set; } = new List<Event>();
        public List<Report> Reports { get; set; } = new List<Report>();

        // Методы для поиска
        public Employee FindEmployeeById(int id)
        {
            return Employees.Find(e => e.Id == id);
        }

        public Client FindClientById(int id)
        {
            return Clients.Find(c => c.Id == id);
        }

        public SecurityContract FindContractByNumber(string number)
        {
            return Contracts.Find(c => c.ContractNumber == number);
        }

        public List<Payment> GetContractPayments(string contractNumber)
        {
            return Payments.FindAll(p => p.Contract?.ContractNumber == contractNumber);
        }

        public decimal GetTotalRevenue()
        {
            decimal total = 0;
            foreach (var contract in Contracts)
            {
                total += contract.TotalAmount;
            }
            return total;
        }

        public int GetActiveEmployeesCount()
        {
            return Employees.FindAll(e => e.IsActive).Count;
        }

        public int GetActiveContractsCount()
        {
            return Contracts.FindAll(c => c.Status == ContractStatus.Active).Count;
        }
    }
}