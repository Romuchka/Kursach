using Kursach;  // Для моделей
using Newtonsoft.Json;
using SecurityCompanyWPF.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Kursach.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();
        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();
        public ObservableCollection<DutySchedule> DutySchedules { get; set; } = new ObservableCollection<DutySchedule>();
        public ObservableCollection<Event> Events { get; set; } = new ObservableCollection<Event>();

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FilteredEmployees));
                OnPropertyChanged(nameof(FilteredClients));
            }
        }

        public ObservableCollection<Employee> FilteredEmployees
        {
            get
            {
                string searchLower = SearchText.ToLower();
                return new ObservableCollection<Employee>(
                    Employees.Where(e => string.IsNullOrEmpty(SearchText) || e.LastName.ToLower().Contains(searchLower) || e.FirstName.ToLower().Contains(searchLower)));
            }
        }

        public ObservableCollection<Client> FilteredClients
        {
            get
            {
                string searchLower = SearchText.ToLower();
                return new ObservableCollection<Client>(
                    Clients.Where(c => string.IsNullOrEmpty(SearchText) || c.FullNameOfCompany.ToLower().Contains(searchLower)));
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee { get => _selectedEmployee; set { _selectedEmployee = value; OnPropertyChanged(); } }

        private Client _selectedClient;
        public Client SelectedClient { get => _selectedClient; set { _selectedClient = value; OnPropertyChanged(); } }

        private DutySchedule _selectedDutySchedule;
        public DutySchedule SelectedDutySchedule { get => _selectedDutySchedule; set { _selectedDutySchedule = value; OnPropertyChanged(); } }

        private Event _selectedEvent;
        public Event SelectedEvent { get => _selectedEvent; set { _selectedEvent = value; OnPropertyChanged(); } }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
            OnPropertyChanged(nameof(FilteredEmployees));
        }

        public void RemoveEmployee(Employee employee)
        {
            Employees.Remove(employee);
            OnPropertyChanged(nameof(FilteredEmployees));
        }

        public void AddClient(Client client)
        {
            Clients.Add(client);
            OnPropertyChanged(nameof(FilteredClients));
        }

        public void RemoveClient(Client client)
        {
            Clients.Remove(client);
            OnPropertyChanged(nameof(FilteredClients));
        }

        public void AddDutySchedule(DutySchedule schedule)
        {
            DutySchedules.Add(schedule);
            OnPropertyChanged(nameof(DutySchedules));
        }

        public void RemoveDutySchedule(DutySchedule schedule)
        {
            DutySchedules.Remove(schedule);
            OnPropertyChanged(nameof(DutySchedules));
        }

        public void AddEvent(Event ev)
        {
            Events.Add(ev);
            OnPropertyChanged(nameof(Events));
        }

        public void RemoveEvent(Event ev)
        {
            Events.Remove(ev);
            OnPropertyChanged(nameof(Events));
        }

        public void SaveData()
        {
            try
            {
                File.WriteAllText("employees.json", JsonConvert.SerializeObject(Employees));
                File.WriteAllText("clients.json", JsonConvert.SerializeObject(Clients));
                File.WriteAllText("schedules.json", JsonConvert.SerializeObject(DutySchedules));
                File.WriteAllText("events.json", JsonConvert.SerializeObject(Events));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка");
            }
        }

        public MainViewModel()
        {
            if (File.Exists("employees.json"))
                Employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(File.ReadAllText("employees.json")) ?? new ObservableCollection<Employee>();
            if (File.Exists("clients.json"))
                Clients = JsonConvert.DeserializeObject<ObservableCollection<Client>>(File.ReadAllText("clients.json")) ?? new ObservableCollection<Client>();
            if (File.Exists("schedules.json"))
                DutySchedules = JsonConvert.DeserializeObject<ObservableCollection<DutySchedule>>(File.ReadAllText("schedules.json")) ?? new ObservableCollection<DutySchedule>();
            if (File.Exists("events.json"))
                Events = JsonConvert.DeserializeObject<ObservableCollection<Event>>(File.ReadAllText("events.json")) ?? new ObservableCollection<Event>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}