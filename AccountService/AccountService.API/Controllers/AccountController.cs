using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
