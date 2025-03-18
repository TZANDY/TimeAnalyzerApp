namespace TimeAnalyzerApp.Models.Domain
{
    public class AttendanceSummary:AttendanceMark
    {
        public string DayOfWeek { get; set; } = string.Empty;
        public TimeSpan TimeLate { get; set; }
        public TimeSpan TimeLateFormatted { get; set; }
        public decimal MinutesLate { get; set; }
        public decimal HoursLate { get; set; }
        public TimeSpan TimeEarly { get; set; }
        public TimeSpan TimeEarlyFormatted { get; set; }
        public decimal MinutesEarly { get; set; }
        public decimal HoursEarly { get; set; }
        public TimeSpan TimeWorked { get; set; }
        public decimal TimeWorkedMinutes { get; set; }
        public decimal TimeWorkedHours { get; set; }
        public decimal TimeExtraHours { get; set; }
        public string TypeEntry { get; set; } = string.Empty;
        public string TypeExit { get; set; } = string.Empty;
        public List<string> Observation { get; set; } = new List<string>();
    }
}
