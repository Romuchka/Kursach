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
    }
}