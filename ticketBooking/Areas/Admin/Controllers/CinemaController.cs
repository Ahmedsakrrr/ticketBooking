using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;


namespace TicketBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CinemaController : Controller
    {
        TicketBookingDbContext _DbContext = new TicketBookingDbContext();
        public IActionResult Index(CinemaMV filter)
        {
            var cinemas = _DbContext.Cinemas.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                cinemas = cinemas.Where(c => c.Name.Contains(filter.Name));

                ViewBag.SearchTerm = filter.Name;
            }
            ViewBag.totalPages = (int)Math.Ceiling(cinemas.Count() / 4.0);
            ViewBag.currentPage = filter.Page;
            cinemas = cinemas.Skip((filter.Page - 1) * 4).Take(4);



            return View(cinemas.AsEnumerable());
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Cinema cinema ,IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + "_" +file.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CimaImage", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                cinema.Img =fileName;

            }
            _DbContext.Cinemas.Add(cinema);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public IActionResult Edit(int id )
        {
            var cinema = _DbContext.Cinemas.FirstOrDefault(c => c.Id == id);
            return View(cinema);
        }
        [HttpPost]
        public IActionResult Edit(Cinema cinema, IFormFile file)
        {
            var cinemaInDb = _DbContext.Cinemas.AsNoTracking().FirstOrDefault(b => b.Id == cinema.Id);
            if(cinemaInDb == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CimaImage", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CimaImage", cinema.Img);
            cinema.Img = fileName;
            if (System.IO.File.Exists(oldpath))
            {
                System.IO.File.Delete(oldpath);
            }
            else
            {
                cinema.Img = cinemaInDb.Img;
            }
            

            _DbContext.Cinemas.Update(cinema);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
      
        public IActionResult Delete(int id)
        {
            var cinema = _DbContext.Cinemas.FirstOrDefault(c => c.Id == id);
            if (cinema == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            var OldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CimaImage", cinema.Img);

            if (System.IO.File.Exists(OldPath))
            {
                System.IO.File.Delete(OldPath);
            }
            _DbContext.Cinemas.Remove(cinema);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}