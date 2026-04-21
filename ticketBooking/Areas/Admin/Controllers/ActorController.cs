using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;


namespace TicketBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ActorController : Controller
    {
        TicketBookingDbContext _DbContext = new TicketBookingDbContext();
        public IActionResult Index(ActorMV filter)
        {
            var actors = _DbContext.Actors.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                actors = actors.Where(c => c.Name.Contains(filter.Name));

                ViewBag.SearchTerm = filter.Name;
            }
            ViewBag.totalPages = (int)Math.Ceiling(actors.Count() / 4.0);
            ViewBag.currentPage = filter.Page;
            actors = actors.Skip((filter.Page - 1) * 4).Take(4);



            return View(actors.AsEnumerable());
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Actor actor ,IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + "_" +file.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ActorsImage", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                actor.Img =fileName;

            }
            _DbContext.Actors.Add(actor);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public IActionResult Edit(int id )
        {
            var actor = _DbContext.Actors.FirstOrDefault(c => c.Id == id);
            return View(actor);
        }
        [HttpPost]
        public IActionResult Edit(Actor actor, IFormFile file)
        {
            var actorInDb = _DbContext.Actors.AsNoTracking().FirstOrDefault(b => b.Id == actor.Id);
            if(actorInDb == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ActorsImage", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            var oldpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ActorsImage", actor.Img);
            actor.Img = fileName;
            if (System.IO.File.Exists(oldpath))
            {
                System.IO.File.Delete(oldpath);
            }
            else
            {
                actor.Img = actorInDb.Img;
            }
            

            _DbContext.Actors.Update(actor);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
      
        public IActionResult Delete(int id)
        {
            var actor = _DbContext.Actors.FirstOrDefault(c => c.Id == id);
            if (actor == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            var OldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ActorsImage", actor.Img);

            if (System.IO.File.Exists(OldPath))
            {
                System.IO.File.Delete(OldPath);
            }
            _DbContext.Actors.Remove(actor);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}