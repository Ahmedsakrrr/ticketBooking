using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ticketBooking.Models;

namespace TicketBooking.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        TicketBookingDbContext _DbContext= new TicketBookingDbContext();
        public IActionResult Index()
        {
            var movies = _DbContext.Movies.AsQueryable();
            movies=movies.Include(m => m.Category).Include(m => m.Cinema).Include(m => m.Actors);
            return View(movies.AsEnumerable());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
