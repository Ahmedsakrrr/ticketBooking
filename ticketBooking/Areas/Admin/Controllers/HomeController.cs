using Microsoft.AspNetCore.Mvc;

namespace TicketBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NotFoundPage()
        {
            return View();
        }
        public IActionResult UnauthorizedPage()
        {
            return View();
        }
        public IActionResult InternalServer()
        {
            return View();
        }
    }
}
