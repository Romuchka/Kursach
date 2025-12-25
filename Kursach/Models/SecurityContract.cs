using System;
using System.Collections.Generic;

namespace SecurityCompanyWPF.Models
{
    public enum ContractType
    {
        ObjectProtection,
        EventProtection,
        PersonalProtection
    }

    public enum ContractStatus
    {
        Active,
        Completed,
        Terminated,
        Draft
    }

    public class SecurityContract
    {
        public string ContractNumber { get; set; }
        public DateTime ContractDate { get; set; } = DateTime.Now;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Client Client { get; set; }
        public ContractType ContractType { get; set; }
        public ContractStatus Status { get; set; } = ContractStatus.Draft;
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public string Description { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();
        public List<Payment> Payments { get; set; } = new List<Payment>();

        public void CreateContract()
        {
            Status = ContractStatus.Active;
            ContractDate = DateTime.Now;
        }

        public void CloseContract()
        {
            Status = ContractStatus.Completed;
            EndDate = DateTime.Now;
        }

        public void TerminateContract()
        {
            Status = ContractStatus.Terminated;
        }

        public decimal GetBalance()
        {
            return TotalAmount - PaidAmount;
        }

        public string ContractInfo =>
            $"Контракт №{ContractNumber} от {ContractDate:dd.MM.yyyy}\n" +
            $"Клиент: {Client?.DisplayName}\n" +
            $"Сумма: {TotalAmount:C}, Статус: {Status}";
    }
}