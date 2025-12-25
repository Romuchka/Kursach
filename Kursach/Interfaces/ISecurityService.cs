using SecurityCompanyWPF.Models;
using System;

namespace SecurityCompanyWPF.Interfaces
{
    public interface ISecurityService
    {
        // Операции с сотрудниками
        OperationResult RegisterEmployee(Employee employee);
        OperationResult UpdateEmployee(Employee employee);
        OperationResult DismissEmployee(int employeeId);

        // Операции с графиком дежурств
        OperationResult CreateDutySchedule(DutySchedule schedule);
        OperationResult UpdateDutySchedule(DutySchedule schedule);
        OperationResult ReplaceEmployee(DutySchedule schedule, Employee newEmployee, string reason);

        // Операции с клиентами
        OperationResult RegisterClient(Client client);
        OperationResult UpdateClient(Client client);

        // Операции с контрактами
        OperationResult CreateContract(SecurityContract contract);
        OperationResult UpdateContract(SecurityContract contract);
        OperationResult CloseContract(string contractNumber);

        // Операции с платежами
        OperationResult RegisterPayment(Payment payment);
        OperationResult UpdatePayment(Payment payment);

        // Отчеты
        Report GenerateFinancialReport(DateTime startDate, DateTime endDate);
        Report GeneratePersonnelReport();
        Report GenerateContractsReport(DateTime startDate, DateTime endDate);

        // Получение данных
        Data GetData();
    }
}