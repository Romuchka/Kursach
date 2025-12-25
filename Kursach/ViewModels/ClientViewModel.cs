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

        public string ContactPerson { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

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