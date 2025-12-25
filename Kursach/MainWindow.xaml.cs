using Kursach;
using Kursach.ViewModels;
using SecurityCompanyWPF.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Kursach
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainViewModel)DataContext;

            // Заполнение ComboBox для расписания
            Loaded += (s, e) =>
            {
                ScheduleEmployeeComboBox.ItemsSource = _viewModel.Employees;
                ScheduleReplacementEmployeeComboBox.ItemsSource = _viewModel.Employees;
            };
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeePanel.Visibility = Visibility.Visible;
            AddClientPanel.Visibility = Visibility.Collapsed;
            AddSchedulePanel.Visibility = Visibility.Collapsed;
            AddEventPanel.Visibility = Visibility.Collapsed;
            MainTabControl.Visibility = Visibility.Collapsed;
        }

        private void SaveEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmployeeLastName.Text) ||
                string.IsNullOrWhiteSpace(EmployeeFirstName.Text) ||
                string.IsNullOrWhiteSpace(EmployeePosition.Text) ||
                string.IsNullOrWhiteSpace(EmployeePhone.Text) ||
                string.IsNullOrWhiteSpace(EmployeeCertificate.Text))
            {
                MessageBox.Show("Заполните все обязательные поля, включая телефон и сертификат", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(EmployeeSalary.Text, out decimal salary))
            {
                MessageBox.Show("Введите корректную зарплату", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var employee = new Employee
            {
                LastName = EmployeeLastName.Text,
                FirstName = EmployeeFirstName.Text,
                Position = EmployeePosition.Text,
                BaseSalary = salary,
                HasWeapon = EmployeeHasWeapon.IsChecked ?? false,
                CertificateNumber = EmployeeCertificate.Text,
                LicenseNumber = $"LIC-{DateTime.Now:yyyyMMddHHmmss}",
                INN = "000000000000",
                PFRNumber = "000-000-000",
                Phone = EmployeePhone.Text
            };

            _viewModel.AddEmployee(employee);

            AddEmployeePanel.Visibility = Visibility.Collapsed;
            MainTabControl.Visibility = Visibility.Visible;

            EmployeeLastName.Text = "Иванов";
            EmployeeFirstName.Text = "Иван";
            EmployeePosition.Text = "Охранник";
            EmployeeSalary.Text = "40000";
            EmployeeHasWeapon.IsChecked = true;
            EmployeePhone.Text = "+7 (999) 000-00-00";
            EmployeeCertificate.Text = "CERT-00000000000000";

            StatusText.Text = "Сотрудник добавлен";
        }

        private void CancelEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeePanel.Visibility = Visibility.Collapsed;
            MainTabControl.Visibility = Visibility.Visible;

            EmployeeLastName.Text = "Иванов";
            EmployeeFirstName.Text = "Иван";
            EmployeePosition.Text = "Охранник";
            EmployeeSalary.Text = "40000";
            EmployeeHasWeapon.IsChecked = true;
            EmployeePhone.Text = "+7 (999) 000-00-00";
            EmployeeCertificate.Text = "CERT-00000000000000";
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            AddClientPanel.Visibility = Visibility.Visible;
            AddEmployeePanel.Visibility = Visibility.Collapsed;
            AddSchedulePanel.Visibility = Visibility.Collapsed;
            AddEventPanel.Visibility = Visibility.Collapsed;
            MainTabControl.Visibility = Visibility.Collapsed;
        }

        private void SaveClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientName.Text) ||
                string.IsNullOrWhiteSpace(ClientAddress.Text))
            {
                MessageBox.Show("Заполните все обязательные поля", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var clientType = ClientTypeComboBox.SelectedIndex == 0 ? ClientType.Individual : ClientType.LegalEntity;

            var client = new Client
            {
                ClientType = clientType,
                FullNameOfCompany = ClientName.Text,
                Address = ClientAddress.Text,
                ContactPerson = ClientContactPerson.Text,
                PassportData = ClientPassportData.Text,
                Phone = ClientPhone.Text,
                Email = ClientEmail.Text
            };

            _viewModel.AddClient(client);

            AddClientPanel.Visibility = Visibility.Collapsed;
            MainTabControl.Visibility = Visibility.Visible;

            ClientName.Text = "";
            ClientAddress.Text = "";
            ClientContactPerson.Text = "";
            ClientPassportData.Text = "";
            ClientPhone.Text = "";
            ClientEmail.Text = "";
            ClientTypeComboBox.SelectedIndex = 0;

            StatusText.Text = "Клиент добавлен";
        }

        private void CancelClientButton_Click(object sender, RoutedEventArgs e)
        {
            AddClientPanel.Visibility = Visibility.Collapsed;
            MainTabControl.Visibility = Visibility.Visible;

            ClientName.Text = "";
            ClientAddress.Text = "";
            ClientContactPerson.Text = "";
            ClientPassportData.Text = "";
            ClientPhone.Text = "";
            ClientEmail.Text = "";
            ClientTypeComboBox.SelectedIndex = 0;
        }

        private void AddScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            AddSchedulePanel.Visibility = Visibility.Visible;
            AddEmployeePanel.Visibility = Visibility.Collapsed;
            AddClientPanel.Visibility = Visibility.Collapsed;
            AddEventPanel.Visibility = Visibility.Collapsed;
            MainTabControl.Visibility = Visibility.Collapsed;

            ScheduleEmployeeComboBox.ItemsSource = _viewModel.Employees;
            ScheduleReplacementEmployeeComboBox.ItemsSource = _viewModel.Employees;
        }

        private void SaveScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            if (ScheduleEmployeeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TimeSpan.TryParse(ScheduleStartTime.Text, out TimeSpan startTime))
            {
                MessageBox.Show("Введите корректное время начала (формат: чч:мм)", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TimeSpan.TryParse(ScheduleEndTime.Text, out TimeSpan endTime))
            {
                MessageBox.Show("Введите корректное время окончания (формат: чч:мм)", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var schedule = new DutySchedule
            {
                Employee = (Employee)ScheduleEmployeeComboBox.SelectedItem,
                DutyDate = ScheduleDatePicker.SelectedDate ?? DateTime.Today,
                StartTime = startTime,
                EndTime = endTime,
                ReplacementEmployee = (Employee)ScheduleReplacementEmployeeComboBox.SelectedItem,
                ReplacementReason = ScheduleReason.Text
            };

            _viewModel.AddDutySchedule(schedule);

            AddSchedulePanel.Visibility = Visibility.Collapsed;
            MainTabControl.Visibility = Visibility.Visible;

            ScheduleEmployeeComboBox.SelectedIndex = -1;
            ScheduleReplacementEmployeeComboBox.SelectedIndex = -1;
            ScheduleDatePicker.SelectedDate = DateTime.Today;
            ScheduleStartTime.Text = "08:00";
            ScheduleEndTime.Text = "20:00";
            ScheduleReason.Text = "";

            StatusText.Text = "Расписание добавлено";
        }

        private void CancelScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            AddSchedulePanel.Visibility = Visibility.Collapsed;
            MainTabControl.Visibility = Visibility.Visible;

            ScheduleEmployeeComboBox.SelectedIndex = -1;
            ScheduleReplacementEmployeeComboBox.SelectedIndex = -1;
            ScheduleDatePicker.SelectedDate = DateTime.Today;
            ScheduleStartTime.Text = "08:00";
            ScheduleEndTime.Text = "20:00";
            ScheduleReason.Text = "";
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            AddEventPanel.Visibility = Visibility.Visible;
            AddEmployeePanel.Visibility = Visibility.Collapsed;
            AddClientPanel.Visibility = Visibility.Collapsed;
            AddSchedulePanel.Visibility = Visibility.Collapsed;
            MainTabControl.Visibility = Visibility.Collapsed;
        }

        private void SaveEventButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EventNameTextBox.Text))
            {
                MessageBox.Show("Введите название события", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TimeSpan.TryParse(EventStartTime.Text, out TimeSpan startTime))
            {
                MessageBox.Show("Введите корректное время начала (формат: чч:мм)", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TimeSpan.TryParse(EventEndTime.Text, out TimeSpan endTime))
            {
                MessageBox.Show("Введите корректное время окончания (формат: чч:мм)", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(EventParticipants.Text, out int participants) || participants <= 0)
            {
                MessageBox.Show("Введите корректное количество участников", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(EventGuardsCount.Text, out int guardsCount) || guardsCount < 0)
            {
                MessageBox.Show("Введите корректное количество охранников", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newEvent = new Event
            {
                EventName = EventNameTextBox.Text,
                EventDate = EventDatePicker.SelectedDate ?? DateTime.Today,
                StartTime = startTime,
                EndTime = endTime,
                Address = EventAddress.Text,
                ParticipantsCount = participants,
                RequiredGuardsCount = guardsCount
            };

            _viewModel.AddEvent(newEvent);

            AddEventPanel.Visibility = Visibility.Collapsed;
            MainTabControl.Visibility = Visibility.Visible;

            EventNameTextBox.Text = "Корпоративное мероприятие";
            EventDatePicker.SelectedDate = DateTime.Today;
            EventStartTime.Text = "18:00";
            EventEndTime.Text = "23:00";
            EventAddress.Text = "Москва, ул. Тверская, 10";
            EventParticipants.Text = "100";
            EventGuardsCount.Text = "2";

            StatusText.Text = "Событие добавлено";
        }

        private void CancelEventButton_Click(object sender, RoutedEventArgs e)
        {
            AddEventPanel.Visibility = Visibility.Collapsed;
            MainTabControl.Visibility = Visibility.Visible;

            EventNameTextBox.Text = "Корпоративное мероприятие";
            EventDatePicker.SelectedDate = DateTime.Today;
            EventStartTime.Text = "18:00";
            EventEndTime.Text = "23:00";
            EventAddress.Text = "Москва, ул. Тверская, 10";
            EventParticipants.Text = "100";
            EventGuardsCount.Text = "2";
        }

        private void DeleteSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedTab = MainTabControl.SelectedItem as TabItem;
            if (selectedTab == null) return;

            switch (selectedTab.Header.ToString())
            {
                case "Сотрудники":
                    if (_viewModel.SelectedEmployee != null)
                    {
                        _viewModel.RemoveEmployee(_viewModel.SelectedEmployee);
                        StatusText.Text = "Сотрудник удален";
                    }
                    break;
                case "Клиенты":
                    if (_viewModel.SelectedClient != null)
                    {
                        _viewModel.RemoveClient(_viewModel.SelectedClient);
                        StatusText.Text = "Клиент удален";
                    }
                    break;
                case "Расписание":
                    if (_viewModel.SelectedDutySchedule != null)
                    {
                        _viewModel.RemoveDutySchedule(_viewModel.SelectedDutySchedule);
                        StatusText.Text = "Расписание удалено";
                    }
                    break;
                case "События":
                    if (_viewModel.SelectedEvent != null)
                    {
                        _viewModel.RemoveEvent(_viewModel.SelectedEvent);
                        StatusText.Text = "Событие удалено";
                    }
                    break;
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveData();
            StatusText.Text = "Данные сохранены";
        }
    }
}