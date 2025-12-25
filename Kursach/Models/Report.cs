using System;
using System.Collections.Generic;

namespace SecurityCompanyWPF.Models
{
    public enum ReportType
    {
        Financial,
        Personnel,
        Contracts,
        All
    }

    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ReportType ReportType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Content { get; set; }
        public DateTime GeneratedDate { get; set; } = DateTime.Now;
        public Dictionary<string, object> Statistics { get; set; } = new Dictionary<string, object>();

        public void AddStatistic(string key, object value)
        {
            Statistics[key] = value;
        }

        public string GetFormattedContent()
        {
            return $"{Title}\n" +
                   $"Период: {StartDate:dd.MM.yyyy} - {EndDate:dd.MM.yyyy}\n" +
                   $"Сформирован: {GeneratedDate:dd.MM.yyyy HH:mm}\n\n" +
                   $"{Content}";
        }
    }
}