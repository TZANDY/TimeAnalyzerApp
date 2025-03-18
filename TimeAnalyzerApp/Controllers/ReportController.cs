using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeAnalyzerApp.Services.Interface;

namespace TimeAnalyzerApp.Controllers
{
    [Authorize]
    public class ReportController : Controller
    {
        private readonly IRecordsService _recordsService;
        public ReportController(IRecordsService recordsService)
        {
            _recordsService = recordsService;
        }

        public IActionResult Index()
        {
            if(User?.Identity?.IsAuthenticated == false)
            {
                RedirectToAction("SignIn", "Access");
            }
            return View();
        }

        public async Task<IActionResult> RecordsPartial(DateTime from, DateTime to)
        {
            var accestoken = User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value ?? string.Empty;
            var records = await _recordsService.GetRecordsByDate(from, to, accestoken);
            return PartialView("_RecordsPartial", records.Data);
        }

        public async Task<IActionResult> SummaryPartial(string recordId, int typeId)
        {
            var accestoken = User.Claims.FirstOrDefault(c => c.Type == "access_token")?.Value ?? string.Empty;
            var summary = await _recordsService.GetAttendanceSummary(recordId, typeId, accestoken);
            return PartialView("_SummaryPartial", summary.Data);
        }

    }
}
