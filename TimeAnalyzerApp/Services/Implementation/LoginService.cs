using TimeAnalyzerApp.Models.Domain;
using TimeAnalyzerApp.Models.ViewModel;
using TimeAnalyzerApp.Services.Interface;

namespace TimeAnalyzerApp.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Response<Access>> Login(LoginVM request)
        {
            var response = await _httpClient.PostAsJsonAsync("Access/login", request);
            return await response.Content.ReadFromJsonAsync<Response<Access>>();
        }
    }
}
