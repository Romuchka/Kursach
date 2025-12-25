using System;

namespace SecurityCompanyWPF.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Address { get; set; }
        public int ParticipantsCount { get; set; }
        public int RequiredGuardsCount { get; set; }

        public int CalculateRequiredGuards()
        {
            // Расчет количества охранников: 1 охранник на 50 человек, минимум 1
            RequiredGuardsCount = Math.Max(1, ParticipantsCount / 50 + 1);
            return RequiredGuardsCount;
        }
    }
}