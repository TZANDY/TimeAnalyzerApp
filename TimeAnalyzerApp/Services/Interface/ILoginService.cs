using TimeAnalyzerApp.Models.Domain;
using TimeAnalyzerApp.Models.ViewModel;

namespace TimeAnalyzerApp.Services.Interface
{
    public interface ILoginService
    {
        Task<Response<Access>> Login(LoginVM request);
    }
}
