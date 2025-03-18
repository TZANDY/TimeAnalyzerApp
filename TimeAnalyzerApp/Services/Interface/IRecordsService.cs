using TimeAnalyzerApp.Models.Domain;
using TimeAnalyzerApp.Models.ViewModel;

namespace TimeAnalyzerApp.Services.Interface
{
    public interface IRecordsService
    {
        Task<Response<AttendanceSummary>> GetAttendanceSummary(string recordId,int typeId,string token);
        Task<Response<string>> GetRecordsByDate(DateTime start, DateTime end,string token);
    }
}
