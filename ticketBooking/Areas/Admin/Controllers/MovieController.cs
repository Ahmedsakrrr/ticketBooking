using Microsoft.AspNetCore.Mvc;

namespace TicketBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        TicketBookingDbContext _DbContext = new TicketBookingDbContext();
        public IActionResult Index(MovieVM filter)
        {
            var movies = _DbContext.Movies.Include(m => m.Actors).Include(m => m.Category).Include(m => m.Cinema).AsQueryable();
            if (filter.Name is not null)
            {
                movies = movies.Where(p => p.Name.Contains(filter.Name));
                ViewBag.Name = filter.Name;   
            }

            if (filter.Price > 0)
            {
                movies = movies.Where(p => p.Price <= filter.Price);
                ViewBag.Price = filter.Price;
            }
            if (filter.CategoryId > 0)
            {
                movies = movies.Where(p => p.CategoryId == filter.CategoryId);
                ViewBag.CategoryId = filter.CategoryId;
            }
            if (filter.CinemaId > 0)
            {
                movies = movies.Where(p => p.CinemaId == filter.CinemaId);
                ViewBag.CinemaId = filter.CinemaId;
            }
            if (filter.ActorId > 0)
            {
                movies = movies.Where(p => p.ActorId == filter.ActorId);
                ViewBag.ActorId = filter.ActorId;
            }
            //if (filter.IsHot)
            //{
            //    movies = movies.Where(p => p.Discount > discount);
            //    ViewBag.IsHot = filter.IsHot;
            //}
            if (filter.Page > 0)
            {
                ViewBag.TotalPages = (int)Math.Ceiling((decimal)movies.Count() / 4);
                movies = movies.Skip((filter.Page - 1) * 4).Take(4);
            }
            ViewBag.Categories = _DbContext.Categories.ToList();
            ViewBag.Cinemas = _DbContext.Cinemas.ToList();
            ViewBag.Actors = _DbContext.Actors.ToList();
            return View(movies.AsEnumerable());
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _DbContext.Categories.ToList();
            ViewBag.Cinemas = _DbContext.Cinemas.ToList();
            ViewBag.Actors = _DbContext.Actors.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Movie movie,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _DbContext.Movies.Add(movie);
                _DbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            if (file is not null && file.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + " _ " + file.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Movieimage", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                movie.MinImg = fileName;
            }
            ViewBag.Categories = _DbContext.Categories.ToList();
            ViewBag.Cinemas = _DbContext.Cinemas.ToList();
            ViewBag.Actors = _DbContext.Actors.ToList();
            _DbContext.Movies.Add(movie);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id, IFormFile file)
        {
            ViewBag.Categories = _DbContext.Categories.ToList();
            ViewBag.Cinemas = _DbContext.Cinemas.ToList();
            ViewBag.Actors = _DbContext.Actors.ToList();
            var movie = _DbContext.Movies.FirstOrDefault(b => b.Id == id);
            if (movie        == null)
            {
                return NotFound();
            }

            return View(movie);

        }
        [HttpPost]
        public IActionResult Edit(Movie movie, IFormFile file)
        {
            var movieInDb = _DbContext.Movies.AsNoTracking().FirstOrDefault(b => b.Id == movie.Id);

            if (movieInDb == null)
                return NotFound();

            if (file is not null && file.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Movieimage", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Movieimage", movieInDb.MinImg);

                movie.MinImg = fileName;

                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }
            }
            else
            {
                movie.MinImg = movieInDb.MinImg;
            }

            _DbContext.Movies.Update(movie);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id, IFormFile file)
        {
            var movie = _DbContext.Movies.FirstOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            var OldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Movieimage", movie.MinImg);
            if (System.IO.File.Exists(OldPath))
            {
                System.IO.File.Delete(OldPath);
            }
            _DbContext.Movies.Remove(movie);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
