using System;
using System.Linq;
using SecurityCompanyWPF.Interfaces;
using SecurityCompanyWPF.Models;

namespace SecurityCompanyWPF.Services
{
    public class SecurityServiceImpl : ISecurityService
    {
        private readonly Data _data;
        private readonly IDataStorage _dataStorage;
        private readonly Random _random = new Random();

        public SecurityServiceImpl(Data data, IDataStorage dataStorage)
        {
            _data = data;
            _dataStorage = dataStorage;
        }

        public OperationResult RegisterEmployee(Employee employee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(employee.LastName) || string.IsNullOrWhiteSpace(employee.FirstName))
                    return OperationResult.Failure("Фамилия и имя обязательны");

                if (_data.Employees.Any(e => e.INN == employee.INN && !string.IsNullOrEmpty(employee.INN)))
                    return OperationResult.Failure("Сотрудник с таким ИНН уже зарегистрирован");

                employee.Id = _data.Employees.Count > 0 ? _data.Employees.Max(e => e.Id) + 1 : 1;
                employee.HireDate = DateTime.Now;
                _data.Employees.Add(employee);
                _dataStorage.SaveData();

                return OperationResult.Success($"Сотрудник {employee.FullName} успешно зарегистрирован", employee);
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при регистрации сотрудника: {ex.Message}");
            }
        }

        public OperationResult UpdateEmployee(Employee employee)
        {
            try
            {
                var existing = _data.FindEmployeeById(employee.Id);
                if (existing == null)
                    return OperationResult.Failure("Сотрудник не найден");

                existing.UpdateEmployee(employee);
                _dataStorage.SaveData();

                return OperationResult.Success($"Данные сотрудника {employee.FullName} обновлены", employee);
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при обновлении сотрудника: {ex.Message}");
            }
        }

        public OperationResult DismissEmployee(int employeeId)
        {
            try
            {
                var employee = _data.FindEmployeeById(employeeId);
                if (employee == null)
                    return OperationResult.Failure("Сотрудник не найден");

                employee.IsActive = false;
                _dataStorage.SaveData();

                return OperationResult.Success($"Сотрудник {employee.FullName} уволен");
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при увольнении сотрудника: {ex.Message}");
            }
        }

        public OperationResult CreateDutySchedule(DutySchedule schedule)
        {
            try
            {
                if (schedule.Employee == null)
                    return OperationResult.Failure("Не указан сотрудник");

                schedule.Id = _data.DutySchedules.Count > 0 ? _data.DutySchedules.Max(d => d.Id) + 1 : 1;
                _data.DutySchedules.Add(schedule);
                _dataStorage.SaveData();

                return OperationResult.Success("График дежурств создан", schedule);
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при создании графика дежурств: {ex.Message}");
            }
        }

        public OperationResult UpdateDutySchedule(DutySchedule schedule)
        {
            try
            {
                var existing = _data.DutySchedules.Find(d => d.Id == schedule.Id);
                if (existing == null)
                    return OperationResult.Failure("График дежурств не найден");

                existing.UpdateSchedule(schedule);
                _dataStorage.SaveData();

                return OperationResult.Success("График дежурств обновлен", schedule);
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при обновлении графика дежурств: {ex.Message}");
            }
        }

        public OperationResult ReplaceEmployee(DutySchedule schedule, Employee newEmployee, string reason)
        {
            try
            {
                var existing = _data.DutySchedules.Find(d => d.Id == schedule.Id);
                if (existing == null)
                    return OperationResult.Failure("График дежурств не найден");

                existing.ReplaceEmployee(newEmployee, reason);
                _dataStorage.SaveData();

                return OperationResult.Success("Сотрудник заменен", existing);
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при замене сотрудника: {ex.Message}");
            }
        }

        public OperationResult RegisterClient(Client client)
        {
            try
            {
                if (client.ClientType == ClientType.LegalEntity && string.IsNullOrWhiteSpace(client.FullNameOfCompany))
                    return OperationResult.Failure("Для юридического лица обязательно название компании");

                if (client.ClientType == ClientType.Individual && string.IsNullOrWhiteSpace(client.PassportData))
                    return OperationResult.Failure("Для физического лица обязательны паспортные данные");

                client.Id = _data.Clients.Count > 0 ? _data.Clients.Max(c => c.Id) + 1 : 1;
                _data.Clients.Add(client);
                _dataStorage.SaveData();

                return OperationResult.Success($"Клиент {client.DisplayName} зарегистрирован", client);
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при регистрации клиента: {ex.Message}");
            }
        }

        public OperationResult UpdateClient(Client client)
        {
            try
            {
                var existing = _data.FindClientById(client.Id);
                if (existing == null)
                    return OperationResult.Failure("Клиент не найден");

                existing.UpdateClient(client);
                _dataStorage.SaveData();

                return OperationResult.Success($"Данные клиента {client.DisplayName} обновлены", client);
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при обновлении клиента: {ex.Message}");
            }
        }

        public OperationResult CreateContract(SecurityContract contract)
        {
            try
            {
                if (contract.Client == null)
                    return OperationResult.Failure("Не указан клиент");

                if (string.IsNullOrWhiteSpace(contract.ContractNumber))
                    contract.ContractNumber = GenerateContractNumber();

                if (_data.Contracts.Any(c => c.ContractNumber == contract.ContractNumber))
                    return OperationResult.Failure("Контракт с таким номером уже существует");

                contract.CreateContract();
                _data.Contracts.Add(contract);
                _dataStorage.SaveData();

                return OperationResult.Success($"Контракт №{contract.ContractNumber} создан", contract);
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при создании контракта: {ex.Message}");
            }
        }

        public OperationResult UpdateContract(SecurityContract contract)
        {
            try
            {
                var existing = _data.FindContractByNumber(contract.ContractNumber);
                if (existing == null)
                    return OperationResult.Failure("Контракт не найден");

                // Обновляем основные поля
                existing.StartDate = contract.StartDate;
                existing.EndDate = contract.EndDate;
                existing.TotalAmount = contract.TotalAmount;
                existing.Description = contract.Description;
                existing.ContractType = contract.ContractType;

                _dataStorage.SaveData();

                return OperationResult.Success($"Контракт №{contract.ContractNumber} обновлен", contract);
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при обновлении контракта: {ex.Message}");
            }
        }

        public OperationResult CloseContract(string contractNumber)
        {
            try
            {
                var contract = _data.FindContractByNumber(contractNumber);
                if (contract == null)
                    return OperationResult.Failure("Контракт не найден");

                contract.CloseContract();
                _dataStorage.SaveData();

                return OperationResult.Success($"Контракт №{contractNumber} закрыт", contract);
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при закрытии контракта: {ex.Message}");
            }
        }

        public OperationResult RegisterPayment(Payment payment)
        {
            try
            {
                if (payment.Contract == null)
                    return OperationResult.Failure("Не указан контракт");

                payment.Id = _data.Payments.Count > 0 ? _data.Payments.Max(p => p.Id) + 1 : 1;
                payment.RegisterPayment();

                // Обновляем сумму оплаты в контракте
                payment.Contract.PaidAmount += payment.Amount;

                _data.Payments.Add(payment);
                _dataStorage.SaveData();

                return OperationResult.Success($"Платеж №{payment.PaymentDocumentNumber} зарегистрирован", payment);
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при регистрации платежа: {ex.Message}");
            }
        }

        public OperationResult UpdatePayment(Payment payment)
        {
            try
            {
                var existing = _data.Payments.Find(p => p.Id == payment.Id);
                if (existing == null)
                    return OperationResult.Failure("Платеж не найден");

                // Сохраняем разницу для обновления суммы в контракте
                var amountDiff = payment.Amount - existing.Amount;

                existing.Amount = payment.Amount;
                existing.PaymentDate = payment.PaymentDate;
                existing.PaymentType = payment.PaymentType;
                existing.Description = payment.Description;

                // Обновляем сумму оплаты в контракте
                if (existing.Contract != null)
                    existing.Contract.PaidAmount += amountDiff;

                _dataStorage.SaveData();

                return OperationResult.Success($"Платеж №{payment.PaymentDocumentNumber} обновлен", payment);
            }
            catch (Exception ex)
            {
                return OperationResult.Error($"Ошибка при обновлении платежа: {ex.Message}");
            }
        }

        public Report GenerateFinancialReport(DateTime startDate, DateTime endDate)
        {
            var report = new Report
            {
                Id = _data.Reports.Count + 1,
                Title = "Финансовый отчет",
                ReportType = ReportType.Financial,
                StartDate = startDate,
                EndDate = endDate,
                GeneratedDate = DateTime.Now
            };

            try
            {
                var paymentsInPeriod = _data.Payments
                    .Where(p => p.PaymentDate >= startDate && p.PaymentDate <= endDate)
                    .ToList();

                decimal totalIncome = paymentsInPeriod.Sum(p => p.Amount);
                int paymentsCount = paymentsInPeriod.Count;

                var newContracts = _data.Contracts
                    .Where(c => c.ContractDate >= startDate && c.ContractDate <= endDate)
                    .ToList();

                decimal contractsValue = newContracts.Sum(c => c.TotalAmount);
                int contractsCount = newContracts.Count;

                var activeContracts = _data.Contracts
                    .Where(c => c.Status == ContractStatus.Active)
                    .ToList();

                decimal activeContractsValue = activeContracts.Sum(c => c.TotalAmount);
                decimal activeContractsPaid = activeContracts.Sum(c => c.PaidAmount);
                decimal activeContractsBalance = activeContractsValue - activeContractsPaid;

                report.Content = $"ФИНАНСОВЫЙ ОТЧЕТ\n" +
                               $"Период: {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy}\n\n" +
                               $"ДОХОДЫ:\n" +
                               $"• Получено платежей: {paymentsCount} на сумму {totalIncome:C}\n\n" +
                               $"КОНТРАКТЫ:\n" +
                               $"• Новых контрактов: {contractsCount} на сумму {contractsValue:C}\n" +
                               $"• Активных контрактов: {activeContracts.Count} на сумму {activeContractsValue:C}\n" +
                               $"• Оплачено по активным: {activeContractsPaid:C}\n" +
                               $"• Задолженность: {activeContractsBalance:C}\n\n" +
                               $"ОБЩАЯ СТАТИСТИКА:\n" +
                               $"• Всего клиентов: {_data.Clients.Count}\n" +
                               $"• Всего контрактов: {_data.Contracts.Count}\n" +
                               $"• Общая выручка: {_data.GetTotalRevenue():C}";

                report.AddStatistic("TotalIncome", totalIncome);
                report.AddStatistic("PaymentsCount", paymentsCount);
                report.AddStatistic("NewContractsCount", contractsCount);
                report.AddStatistic("NewContractsValue", contractsValue);

                _data.Reports.Add(report);
                _dataStorage.SaveData();

                return report;
            }
            catch (Exception ex)
            {
                report.Content = $"Ошибка при генерации отчета: {ex.Message}";
                return report;
            }
        }

        public Report GeneratePersonnelReport()
        {
            var report = new Report
            {
                Id = _data.Reports.Count + 1,
                Title = "Кадровый отчет",
                ReportType = ReportType.Personnel,
                StartDate = DateTime.Now.AddMonths(-1),
                EndDate = DateTime.Now,
                GeneratedDate = DateTime.Now
            };

            try
            {
                var activeEmployees = _data.Employees.Where(e => e.IsActive).ToList();
                var inactiveEmployees = _data.Employees.Where(e => !e.IsActive).ToList();

                decimal totalSalary = activeEmployees.Sum(e => e.CalculateSalary());
                decimal avgSalary = activeEmployees.Count > 0 ? totalSalary / activeEmployees.Count : 0;

                var employeesByPosition = activeEmployees
                    .GroupBy(e => e.Position)
                    .Select(g => new { Position = g.Key, Count = g.Count() })
                    .ToList();

                report.Content = $"КАДРОВЫЙ ОТЧЕТ\n" +
                               $"Дата формирования: {DateTime.Now:dd.MM.yyyy HH:mm}\n\n" +
                               $"СОТРУДНИКИ:\n" +
                               $"• Активных: {activeEmployees.Count}\n" +
                               $"• Неактивных: {inactiveEmployees.Count}\n" +
                               $"• Всего: {_data.Employees.Count}\n\n" +
                               $"ФОНД ЗАРАБОТНОЙ ПЛАТЫ:\n" +
                               $"• Общий: {totalSalary:C}\n" +
                               $"• Средняя ЗП: {avgSalary:C}\n\n" +
                               $"РАСПРЕДЕЛЕНИЕ ПО ДОЛЖНОСТЯМ:\n";

                foreach (var group in employeesByPosition)
                {
                    report.Content += $"• {group.Position}: {group.Count} чел.\n";
                }

                report.AddStatistic("ActiveEmployees", activeEmployees.Count);
                report.AddStatistic("TotalSalary", totalSalary);
                report.AddStatistic("AverageSalary", avgSalary);

                _data.Reports.Add(report);
                _dataStorage.SaveData();

                return report;
            }
            catch (Exception ex)
            {
                report.Content = $"Ошибка при генерации отчета: {ex.Message}";
                return report;
            }
        }

        public Report GenerateContractsReport(DateTime startDate, DateTime endDate)
        {
            var report = new Report
            {
                Id = _data.Reports.Count + 1,
                Title = "Отчет по контрактам",
                ReportType = ReportType.Contracts,
                StartDate = startDate,
                EndDate = endDate,
                GeneratedDate = DateTime.Now
            };

            try
            {
                var contractsInPeriod = _data.Contracts
                    .Where(c => c.ContractDate >= startDate && c.ContractDate <= endDate)
                    .ToList();

                var contractsByType = contractsInPeriod
                    .GroupBy(c => c.ContractType)
                    .Select(g => new { Type = g.Key, Count = g.Count(), Sum = g.Sum(c => c.TotalAmount) })
                    .ToList();

                var contractsByStatus = contractsInPeriod
                    .GroupBy(c => c.Status)
                    .Select(g => new { Status = g.Key, Count = g.Count(), Sum = g.Sum(c => c.TotalAmount) })
                    .ToList();

                report.Content = $"ОТЧЕТ ПО КОНТРАКТАМ\n" +
                               $"Период: {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy}\n\n" +
                               $"ВСЕГО КОНТРАКТОВ ЗА ПЕРИОД: {contractsInPeriod.Count} на сумму {contractsInPeriod.Sum(c => c.TotalAmount):C}\n\n" +
                               $"ПО ТИПАМ:\n";

                foreach (var group in contractsByType)
                {
                    report.Content += $"• {group.Type}: {group.Count} шт. на сумму {group.Sum:C}\n";
                }

                report.Content += $"\nПО СТАТУСАМ:\n";
                foreach (var group in contractsByStatus)
                {
                    report.Content += $"• {group.Status}: {group.Count} шт. на сумму {group.Sum:C}\n";
                }

                report.AddStatistic("TotalContracts", contractsInPeriod.Count);
                report.AddStatistic("TotalValue", contractsInPeriod.Sum(c => c.TotalAmount));

                _data.Reports.Add(report);
                _dataStorage.SaveData();

                return report;
            }
            catch (Exception ex)
            {
                report.Content = $"Ошибка при генерации отчета: {ex.Message}";
                return report;
            }
        }

        public Data GetData()
        {
            return _data;
        }

        private string GenerateContractNumber()
        {
            return $"CT-{DateTime.Now:yyyyMMdd}-{_random.Next(1000, 9999)}";
        }
    }
}