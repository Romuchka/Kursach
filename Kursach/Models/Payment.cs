using System;

namespace SecurityCompanyWPF.Models
{
    public enum PaymentType
    {
        Cash,
        BankTransfer,
        Card
    }

    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public PaymentType PaymentType { get; set; }
        public string PaymentDocumentNumber { get; set; }
        public SecurityContract Contract { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; } = true;

        public void RegisterPayment()
        {
            IsPaid = true;
            PaymentDate = DateTime.Now;
        }

        public string GenerateFinancialReport()
        {
            return $"Платеж №{PaymentDocumentNumber} от {PaymentDate:dd.MM.yyyy}\n" +
                   $"Сумма: {Amount:C}\n" +
                   $"Тип: {PaymentType}\n" +
                   $"Контракт: {Contract?.ContractNumber ?? "Не указан"}";
        }

        public string ShortInfo => $"№{PaymentDocumentNumber} - {Amount:C} ({PaymentDate:dd.MM.yyyy})";
    }
}