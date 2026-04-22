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
            _DbContext.Books.Add(book);
            _DbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var book = _DbContext.Books.FirstOrDefault(x => x.Id == id);

            ViewBag.Movies = _DbContext.Movies.ToList(); // مهم جدًا
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            var bookInDb = _DbContext.Books.AsNoTracking().FirstOrDefault(b => b.Id == book.Id);
            if (bookInDb == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            


            _DbContext.Books.Update(book);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var book = _DbContext.Books.FirstOrDefault(c => c.Id == id);
            if (book == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            
            _DbContext.Books.Remove(book);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}