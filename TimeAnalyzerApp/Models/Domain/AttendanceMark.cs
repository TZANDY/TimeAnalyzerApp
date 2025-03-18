namespace TimeAnalyzerApp.Models.Domain
{
    public class AttendanceMark
    {
        public string RecordNumber { get; set; } = string.Empty;
        public string EmployeeCardNumber { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TimeSpan FirstMarking { get; set; }
        public TimeSpan LastMarking { get; set; }
        public string Area { get; set; } = string.Empty;
    }
}
