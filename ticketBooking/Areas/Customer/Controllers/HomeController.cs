using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ticketBooking.Models;
using static System.Net.WebRequestMethods;

namespace TicketBooking.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        TicketBookingDbContext _DbContext= new TicketBookingDbContext();
        public IActionResult Index(int Page = 1)
        {
            var movies = _DbContext.Movies.AsQueryable();
            movies = movies.Include(m => m.Category).Include(m => m.Cinema).Include(m => m.Actors);
            if (Page > 0)
            {
                ViewBag.TotalPages = (int)Math.Ceiling((decimal)movies.Count() / 4);
                movies = movies.Skip((Page - 1) * 4).Take(4);
            }
            
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
