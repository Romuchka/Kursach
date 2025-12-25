using System;

namespace SecurityCompanyWPF.Models
{
    public enum ClientType
    {
        Individual,
        LegalEntity
    }

    public class Client
    {
        public int Id { get; set; }
        public ClientType ClientType { get; set; }
        public string FullNameOfCompany { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PassportData { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        public void UpdateClient(Client updatedClient)
        {
            ClientType = updatedClient.ClientType;
            FullNameOfCompany = updatedClient.FullNameOfCompany;
            ContactPerson = updatedClient.ContactPerson;
            Address = updatedClient.Address;
            Phone = updatedClient.Phone;
            Email = updatedClient.Email;
            PassportData = updatedClient.PassportData;
            IsActive = updatedClient.IsActive;
        }

        public string DisplayName => ClientType == ClientType.LegalEntity
            ? FullNameOfCompany
            : $"{PassportData} ({ContactPerson})";
    }
}