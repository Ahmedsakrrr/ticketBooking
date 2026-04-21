using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;


namespace TicketBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        TicketBookingDbContext _DbContext = new TicketBookingDbContext();
        public IActionResult Index(CategoryVM filter)
        {
            var categories = _DbContext.Categories.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                categories = categories.Where(c => c.Name.Contains(filter.Name));

                ViewBag.SearchTerm = filter.Name;
            }
            ViewBag.totalPages = (int)Math.Ceiling(categories.Count() / 4.0);
            ViewBag.currentPage = filter.Page;
            categories = categories.Skip((filter.Page - 1) * 4).Take(4);



            return View(categories.AsEnumerable());
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            _DbContext.Categories.Add(category);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _DbContext.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {

            _DbContext.Categories.Update(category);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
      
        public IActionResult Delete(int id)
        {
            var category = _DbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            _DbContext.Categories.Remove(category);
            _DbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}