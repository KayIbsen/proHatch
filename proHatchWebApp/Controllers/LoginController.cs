using Microsoft.AspNetCore.Mvc;
using proHatchWebApp.Models;

namespace proHatchWebApp.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {

        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login_DTO login_DTO)
        {
            return View();
        }

    }
}
