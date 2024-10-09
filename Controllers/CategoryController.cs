using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context {  get; set; }

        public CategoryController(ApplicationDbContext context) {
            _context = context; 
        }
        public IActionResult Index()
        {
            return View(_context.Categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Naam en volgnummer mogen niet hetzelfde zijn");
            }

            if (_context.Categories.Any(c => c.Name == category.Name))
            {
                ModelState.AddModelError("uniqueName", "Deze category bestaat al");
            }

            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                try
                {
                    _context.SaveChanges();
					TempData["result"] = $"Categorie {category.Name} succesvol toegevoegd.";

				}
				catch (Exception ex)
                {
                    ViewBag.Error = "Er is een probleem met de database!";
                    return View(category);
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        
        public IActionResult Edit(int? id) 
        {

			if (id == null)
            {
                return NotFound();
            }

			Category category = _context.Categories.Find(id);

			if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
			if (category.Name == category.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "Naam en volgnummer mogen niet hetzelfde zijn");
			}

			if (ModelState.IsValid)
			{
				_context.Categories.Update(category);
				try
				{
					_context.SaveChanges();
					TempData["result"] = $"Categorie {category.Name} succesvol aangepast";

				}
				catch (Exception ex)
				{
					ViewBag.Error = "Er is een probleem met de database!";
					return View(category);
				}
				return RedirectToAction("Index");
			}
			return View(category);
		}

        public ActionResult Delete(int? id)
        { 
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult ConfirmDelete(Category category)
        {
            _context.Categories.Remove(category);
            try
            {
                _context.SaveChanges();
                TempData["result"] = $"Categorie {category.Name} succesvol verwijderd";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Er is een probleem met de database!";
                return View(category);
            }
            return RedirectToAction("Index");
        }

        
	}
}
