using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TimeAnalyzerApp.Services.Interface;
using TimeAnalyzerApp.Models.ViewModel;

namespace TimeAnalyzerApp.Controllers
{
    public class AccessController : Controller
    {
        private readonly ILoginService _loginService;

        public AccessController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public ActionResult SignIn()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ResetCookie()
        {
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginVM request)
        {
            var response = await _loginService.Login(request);
            if (response.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim("access_token", response.Result.Token),
                    new Claim(ClaimTypes.Name, response.Result.Name),
                    new Claim(ClaimTypes.Role, response.Result.Role ?? "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("SignIn", "Access");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SignIn", "Access");
        }
    }
}
