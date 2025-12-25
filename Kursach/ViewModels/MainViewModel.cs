using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SecurityCompanyWPF.Models;
using SecurityCompanyWPF.Services;

namespace SecurityCompanyWPF.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly DataStorage _dataStorage;

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                FilterData();
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Employee> FilteredEmployees { get; set; }

        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Client> FilteredClients { get; set; }

        public ObservableCollection<DutySchedule> DutySchedules { get; set; }
        public ObservableCollection<Event> Events { get; set; }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        private DutySchedule _selectedDutySchedule;
        public DutySchedule SelectedDutySchedule
        {
            get => _selectedDutySchedule;
            set
            {
                _selectedDutySchedule = value;
                OnPropertyChanged(nameof(SelectedDutySchedule));
            }
        }

        private Event _selectedEvent;
        public Event SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
            }
        }

        private string _statusMessage = "Система готова к работе";
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        }

        public MainViewModel()
        {
            _dataStorage = new DataStorage();
            _dataStorage.LoadData();

            var data = _dataStorage.GetData();

            Employees = new ObservableCollection<Employee>(data.Employees);
            Clients = new ObservableCollection<Client>(data.Clients);
            DutySchedules = new ObservableCollection<DutySchedule>(data.DutySchedules);
            Events = new ObservableCollection<Event>(data.Events);

            FilteredEmployees = new ObservableCollection<Employee>(Employees);
            FilteredClients = new ObservableCollection<Client>(Clients);

            StatusMessage = $"Загружено {Employees.Count} сотрудников, {Clients.Count} клиентов, " +
                          $"{DutySchedules.Count} расписаний, {Events.Count} событий";
        }

        private void FilterData()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredEmployees.Clear();
                foreach (var emp in Employees)
                    FilteredEmployees.Add(emp);

                FilteredClients.Clear();
                foreach (var cli in Clients)
                    FilteredClients.Add(cli);
            }
            else
            {
                var search = SearchText.ToLower();

                FilteredEmployees.Clear();
                foreach (var emp in Employees.Where(e =>
                    e.FullName.ToLower().Contains(search) ||
                    e.Position.ToLower().Contains(search) ||
                    e.CertificateNumber.ToLower().Contains(search)))
                {
                    FilteredEmployees.Add(emp);
                }

                FilteredClients.Clear();
                foreach (var cli in Clients.Where(c =>
                    c.DisplayName.ToLower().Contains(search) ||
                    c.Address.ToLower().Contains(search) ||
                    (c.Phone ?? "").Contains(search)))
                {
                    FilteredClients.Add(cli);
                }
            }
        }

        public void AddEmployee(Employee employee)
        {
            employee.Id = Employees.Count > 0 ? Employees.Max(e => e.Id) + 1 : 1;
            Employees.Add(employee);
            FilterData();
            StatusMessage = $"Добавлен сотрудник: {employee.FullName} с зарплатой {employee.CalculateSalary():C}";
        }

        public void AddClient(Client client)
        {
            client.Id = Clients.Count > 0 ? Clients.Max(c => c.Id) + 1 : 1;
            Clients.Add(client);
            FilterData();
            StatusMessage = $"Добавлен клиент: {client.DisplayName}";
        }

        public void AddDutySchedule(DutySchedule schedule)
        {
            schedule.Id = DutySchedules.Count > 0 ? DutySchedules.Max(d => d.Id) + 1 : 1;
            DutySchedules.Add(schedule);
            StatusMessage = $"Добавлено расписание для {schedule.Employee.FullName} на {schedule.DutyDate:dd.MM.yyyy}";
        }

        public void AddEvent(Event newEvent)
        {
            newEvent.Id = Events.Count > 0 ? Events.Max(ev => ev.Id) + 1 : 1;
            Events.Add(newEvent);
            StatusMessage = $"Добавлено событие: {newEvent.EventName} ({newEvent.RequiredGuardsCount} охранников)";
        }

        public void SaveData()
        {
            _dataStorage.SaveData();
            StatusMessage = "Все данные сохранены";
        }
    }
}