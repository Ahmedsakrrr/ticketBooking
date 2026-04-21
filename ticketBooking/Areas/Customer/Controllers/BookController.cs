using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TicketBooking.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BookController : Controller
    {
        TicketBookingDbContext _DbContext = new TicketBookingDbContext();

        public IActionResult Index()
        {
            var books = _DbContext.Books.Include(b => b.Movie).AsQueryable();
            return View(books.AsEnumerable());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Movies = _DbContext.Movies.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            var movie = _DbContext.Movies.FirstOrDefault(m => m.Id == book.MovieId);

            if (movie != null)
            {
                book.Movie = movie;           // ربط الفيلم
                book.Movie.Price = movie.Price;
                book.Movie.Description = movie.Description;
                book.Movie.DateTime = movie.DateTime;
            }

            _DbContext.Books.Add(book);
            _DbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}