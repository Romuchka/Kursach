using System;
using System.Windows;
using SecurityCompanyWPF.ViewModels;
using SecurityCompanyWPF.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace SecurityCompanyWPF
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            // Заполняем ComboBox сотрудников для расписания
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Заполняем ComboBox сотрудников для расписания
            ScheduleEmployeeComboBox.ItemsSource = _viewModel.Employees;
        }

        // ========== СОТРУДНИКИ ==========

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            // Скрываем другие панели
            AddClientPanel.Visibility = Visibility.Collapsed;
            AddSchedulePanel.Visibility = Visibility.Collapsed;
            AddEventPanel.Visibility = Visibility.Collapsed;

            // Показываем панель сотрудника
            AddEmployeePanel.Visibility = Visibility.Visible;

            // Устанавливаем фокус
            EmployeeLastName.Focus();
            EmployeeLastName.SelectAll();
        }

        private void SaveEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmployeeLastName.Text) ||
                string.IsNullOrWhiteSpace(EmployeeFirstName.Text) ||
                string.IsNullOrWhiteSpace(EmployeePosition.Text))
            {
                MessageBox.Show("Заполните все обязательные поля", "Ошибка",
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
                CertificateNumber = $"CERT-{DateTime.Now:yyyyMMddHHmmss}",
                LicenseNumber = $"LIC-{DateTime.Now:yyyyMMddHHmmss}",
                INN = "000000000000",
                PFRNumber = "000-000-000",
                Phone = "+7 (999) 000-00-00"
            };

            _viewModel.AddEmployee(employee);

            AddEmployeePanel.Visibility = Visibility.Collapsed;

            // Очищаем поля
            EmployeeLastName.Text = "";
            EmployeeFirstName.Text = "";
            EmployeePosition.Text = "";
            EmployeeSalary.Text = "40000";
            EmployeeHasWeapon.IsChecked = true;
        }

        private void CancelEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeePanel.Visibility = Visibility.Collapsed;

            // Очищаем поля
            EmployeeLastName.Text = "";
            EmployeeFirstName.Text = "";
            EmployeePosition.Text = "";
            EmployeeSalary.Text = "40000";
            EmployeeHasWeapon.IsChecked = true;
        }

        // ========== КЛИЕНТЫ ==========

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            // Скрываем другие панели
            AddEmployeePanel.Visibility = Visibility.Collapsed;
            AddSchedulePanel.Visibility = Visibility.Collapsed;
            AddEventPanel.Visibility = Visibility.Collapsed;

            // Показываем панель клиента
            AddClientPanel.Visibility = Visibility.Visible;

            // Устанавливаем фокус
            ClientName.Focus();
            ClientName.SelectAll();
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

            var clientType = ClientTypeComboBox.SelectedIndex == 0 ?
                ClientType.Individual : ClientType.LegalEntity;

            var client = new Client
            {
                ClientType = clientType,
                FullNameOfCompany = ClientName.Text,
                Address = ClientAddress.Text,
                Phone = "+7 (999) 000-00-00",
                Email = "info@example.com"
            };

            _viewModel.AddClient(client);

            AddClientPanel.Visibility = Visibility.Collapsed;

            // Очищаем поля
            ClientName.Text = "";
            ClientAddress.Text = "";
            ClientTypeComboBox.SelectedIndex = 0;
        }

        private void CancelClientButton_Click(object sender, RoutedEventArgs e)
        {
            AddClientPanel.Visibility = Visibility.Collapsed;

            // Очищаем поля
            ClientName.Text = "";
            ClientAddress.Text = "";
            ClientTypeComboBox.SelectedIndex = 0;
        }

        // ========== РАСПИСАНИЕ ==========

        private void AddScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            // Скрываем другие панели
            AddEmployeePanel.Visibility = Visibility.Collapsed;
            AddClientPanel.Visibility = Visibility.Collapsed;
            AddEventPanel.Visibility = Visibility.Collapsed;

            // Обновляем список сотрудников в ComboBox
            ScheduleEmployeeComboBox.ItemsSource = _viewModel.Employees;

            // Показываем панель расписания
            AddSchedulePanel.Visibility = Visibility.Visible;

            // Устанавливаем фокус
            if (ScheduleEmployeeComboBox.Items.Count > 0)
                ScheduleEmployeeComboBox.SelectedIndex = 0;
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
                ReplacementReason = ScheduleReason.Text
            };

            _viewModel.AddDutySchedule(schedule);

            AddSchedulePanel.Visibility = Visibility.Collapsed;

            // Очищаем поля
            ScheduleEmployeeComboBox.SelectedIndex = -1;
            ScheduleDatePicker.SelectedDate = DateTime.Today;
            ScheduleStartTime.Text = "08:00";
            ScheduleEndTime.Text = "20:00";
            ScheduleReason.Text = "";
        }

        private void CancelScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            AddSchedulePanel.Visibility = Visibility.Collapsed;

            // Очищаем поля
            ScheduleEmployeeComboBox.SelectedIndex = -1;
            ScheduleDatePicker.SelectedDate = DateTime.Today;
            ScheduleStartTime.Text = "08:00";
            ScheduleEndTime.Text = "20:00";
            ScheduleReason.Text = "";
        }

        // ========== СОБЫТИЯ ==========

        private void AddEventButton_Click(object sender, RoutedEventArgs e)
        {
            // Скрываем другие панели
            AddEmployeePanel.Visibility = Visibility.Collapsed;
            AddClientPanel.Visibility = Visibility.Collapsed;
            AddSchedulePanel.Visibility = Visibility.Collapsed;

            // Показываем панель события
            AddEventPanel.Visibility = Visibility.Visible;

            // Устанавливаем фокус
            EventNameTextBox.Focus();
            EventNameTextBox.SelectAll();
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

            var newEvent = new Event
            {
                EventName = EventNameTextBox.Text,
                EventDate = EventDatePicker.SelectedDate ?? DateTime.Today,
                StartTime = startTime,
                EndTime = endTime,
                Address = EventAddress.Text,
                ParticipantsCount = participants
            };

            _viewModel.AddEvent(newEvent);

            AddEventPanel.Visibility = Visibility.Collapsed;

            // Очищаем поля
            EventNameTextBox.Text = "Корпоративное мероприятие";
            EventDatePicker.SelectedDate = DateTime.Today;
            EventStartTime.Text = "18:00";
            EventEndTime.Text = "23:00";
            EventAddress.Text = "Москва, ул. Тверская, 10";
            EventParticipants.Text = "100";
        }

        private void CancelEventButton_Click(object sender, RoutedEventArgs e)
        {
            AddEventPanel.Visibility = Visibility.Collapsed;

            // Очищаем поля
            EventNameTextBox.Text = "Корпоративное мероприятие";
            EventDatePicker.SelectedDate = DateTime.Today;
            EventStartTime.Text = "18:00";
            EventEndTime.Text = "23:00";
            EventAddress.Text = "Москва, ул. Тверская, 10";
            EventParticipants.Text = "100";
        }

        // ========== ОБЩИЕ ФУНКЦИИ ==========

        private void SaveDataButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveData();
        }
    }
}