using System;
using System.Collections.Generic;
using SecurityCompanyWPF.Models;

namespace SecurityCompanyWPF.Services
{
    public class DataStorage
    {
        private Data _data = new Data();

        public void LoadData()
        {
            // Если данных нет, создаем тестовые
            if (_data.Employees.Count == 0)
            {
                CreateSampleData();
            }
        }

        public void SaveData()
        {
            // В этой версии просто сохраняем в памяти
            Console.WriteLine($"Данные сохранены: {_data.Employees.Count} сотрудников, " +
                            $"{_data.Clients.Count} клиентов, {_data.DutySchedules.Count} расписаний, " +
                            $"{_data.Events.Count} событий");
        }

        public Data GetData()
        {
            return _data;
        }

        private void CreateSampleData()
        {
            // Тестовые сотрудники
            var employee1 = new Employee
            {
                Id = 1,
                LastName = "Иванов",
                FirstName = "Иван",
                Position = "Старший охранник",
                BaseSalary = 50000,
                Allowance = 10000,
                HasWeapon = true,
                CertificateNumber = "CERT-001",
                Phone = "+7 (999) 123-45-67"
            };

            var employee2 = new Employee
            {
                Id = 2,
                LastName = "Петров",
                FirstName = "Петр",
                Position = "Охранник",
                BaseSalary = 40000,
                Allowance = 5000,
                HasWeapon = true,
                CertificateNumber = "CERT-002",
                Phone = "+7 (999) 234-56-78"
            };

            _data.Employees.AddRange(new[] { employee1, employee2 });

            // Тестовые клиенты
            _data.Clients.Add(new Client
            {
                Id = 1,
                ClientType = ClientType.LegalEntity,
                FullNameOfCompany = "ООО 'Ромашка'",
                Address = "Москва, пр. Мира, д. 100",
                Phone = "+7 (495) 123-45-67",
                Email = "info@romashka.ru"
            });

            _data.Clients.Add(new Client
            {
                Id = 2,
                ClientType = ClientType.Individual,
                Address = "Москва, ул. Садовая, д. 25",
                PassportData = "4500 123456",
                Phone = "+7 (999) 345-67-89",
                Email = "kuznetsov@mail.ru"
            });

            // Тестовые расписания
            _data.DutySchedules.Add(new DutySchedule
            {
                Id = 1,
                Employee = employee1,
                DutyDate = DateTime.Today.AddDays(1),
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(20, 0, 0)
            });

            _data.DutySchedules.Add(new DutySchedule
            {
                Id = 2,
                Employee = employee2,
                DutyDate = DateTime.Today.AddDays(2),
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(20, 0, 0),
                ReplacementReason = "Болезнь"
            });

            // Тестовые события
            _data.Events.Add(new Event
            {
                Id = 1,
                EventName = "Корпоративное мероприятие",
                EventDate = DateTime.Today.AddDays(5),
                StartTime = new TimeSpan(18, 0, 0),
                EndTime = new TimeSpan(23, 0, 0),
                Address = "Москва, ул. Тверская, д. 10",
                ParticipantsCount = 100
            });

            _data.Events.Add(new Event
            {
                Id = 2,
                EventName = "Конференция по безопасности",
                EventDate = DateTime.Today.AddDays(10),
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(18, 0, 0),
                Address = "Москва, пр. Мира, д. 100",
                ParticipantsCount = 200
            });
        }
    }
}