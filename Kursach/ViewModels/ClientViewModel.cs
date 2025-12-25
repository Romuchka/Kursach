using SecurityCompanyWPF.Models;

namespace SecurityCompanyWPF.ViewModels
{
    public class ClientViewModel : ObservableObject
    {
        private Client _client;

        public ClientType ClientType
        {
            get => _client.ClientType;
            set
            {
                _client.ClientType = value;
                OnPropertyChanged(nameof(ClientType));
                OnPropertyChanged(nameof(IsIndividual));
                OnPropertyChanged(nameof(IsLegalEntity));
            }
        }

        public string FullNameOfCompany
        {
            get => _client.FullNameOfCompany;
            set
            {
                _client.FullNameOfCompany = value;
                OnPropertyChanged(nameof(FullNameOfCompany));
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public string Address
        {
            get => _client.Address;
            set
            {
                _client.Address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string PassportData
        {
            get => _client.PassportData;
            set
            {
                _client.PassportData = value;
                OnPropertyChanged(nameof(PassportData));
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public string ContactPerson
        {
            get => _client.ContactPerson;
            set
            {
                _client.ContactPerson = value;
                OnPropertyChanged(nameof(ContactPerson));
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public string Phone
        {
            get => _client.Phone;
            set
            {
                _client.Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        public string Email
        {
            get => _client.Email;
            set
            {
                _client.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public bool IsIndividual => ClientType == Models.ClientType.Individual;
        public bool IsLegalEntity => ClientType == Models.ClientType.LegalEntity;
        public string DisplayName => _client.DisplayName;

        public Client Client => _client;

        public ClientViewModel(Client client = null)
        {
            _client = client ?? new Client();
        }
    }
}