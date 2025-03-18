using System.Net.Http.Headers;
using TimeAnalyzerApp.Models.Domain;
using TimeAnalyzerApp.Models.ViewModel;
using TimeAnalyzerApp.Services.Interface;

namespace TimeAnalyzerApp.Services.Implementation
{
    public class RecordService : IRecordsService
    {
        private readonly HttpClient _httpClient;
        public RecordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<AttendanceSummary>> GetAttendanceSummary(string recordId, int typeId, string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"AttendanceMark/records/{recordId}/type/{typeId}");
                if (response.IsSuccessStatusCode)
                {
                    var summary = await response.Content.ReadFromJsonAsync<Response<AttendanceSummary>>();
                    if (summary != null)
                    {
                        return summary;
                    }
                }
                return new Response<AttendanceSummary>();
            }
            catch (Exception ex)
            {
                return new Response<AttendanceSummary>()
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<string>> GetRecordsByDate(DateTime startDate, DateTime endDate, string token)
        {
            try
            {
                var formatStartDate = startDate.ToString("yyyyMMdd");
                var formatEndDate = endDate.ToString("yyyyMMdd");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.GetAsync($"AttendanceMark/records/start/{formatStartDate}/end/{formatEndDate}");
                if (response.IsSuccessStatusCode)
                {
                    var records = await response.Content.ReadFromJsonAsync<Response<string>>();
                    if (records != null)
                    {
                        return records;
                    }
                }
                return new Response<string>();
            }
            catch (Exception e)
            {
                return new Response<string>()
                {
                    Message = e.Message
                };
            }
        }
    }
}
