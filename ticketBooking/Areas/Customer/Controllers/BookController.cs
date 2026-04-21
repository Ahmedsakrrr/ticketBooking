using Microsoft.AspNetCore.Mvc;

namespace TicketBooking.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BookController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
