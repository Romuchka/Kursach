using System;

namespace SecurityCompanyWPF.Models
{
    public class DutySchedule
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public DateTime DutyDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Employee ReplacementEmployee { get; set; }
        public string ReplacementReason { get; set; }

        // Новый метод для обновления графика
        public void UpdateSchedule(DutySchedule other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            this.Employee = other.Employee;
            this.DutyDate = other.DutyDate;
            this.StartTime = other.StartTime;
            this.EndTime = other.EndTime;
            this.ReplacementEmployee = other.ReplacementEmployee;
            this.ReplacementReason = other.ReplacementReason;
        }

        // Новый метод для замены сотрудника
        public void ReplaceEmployee(Employee newEmployee, string reason)
        {
            if (newEmployee == null) throw new ArgumentNullException(nameof(newEmployee));
            if (string.IsNullOrWhiteSpace(reason)) throw new ArgumentException("Причина замены обязательна", nameof(reason));

            // Логика замены: устанавливаем заменяющего сотрудника и причину
            // Если нужно заменить основного сотрудника, добавьте this.Employee = newEmployee;
            // Но судя по полям класса, replacement — это отдельная замена (возможно, временная)
            this.ReplacementEmployee = newEmployee;
            this.ReplacementReason = reason;
        }
    }
}