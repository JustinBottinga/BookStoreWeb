using BookStore.Models;
using BookStore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using BookStore.DataAccess.Repository;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICoverTypeRepository _coverTypeRepository;

        public CoverTypeController(IUnitOfWork context)
        {
            _unitOfWork = context;
            _coverTypeRepository = _unitOfWork.CoverType;
        }

        public IActionResult Index()
        {
            return View(_coverTypeRepository.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {

            if (_coverTypeRepository.GetFirstOrDefault(c => c.Name == coverType.Name) != null)
            {
                ModelState.AddModelError("name", "Deze categorie bestaat al");
            }

            if (ModelState.IsValid)
            {
                _coverTypeRepository.Add(coverType);
                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = $"Kaftsoort {coverType.Name} is toegevoegd";
                }
                catch
                {
                    ViewBag.Error = "Er is een probleem met de database!";
                }
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _coverTypeRepository.GetFirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(CoverType coverType) // Ensure the correct namespace is used
        {

            if (ModelState.IsValid)
            {
                _coverTypeRepository.Update(coverType);

                try
                {
                    _unitOfWork.Save();
                    TempData["result"] = $"Kaftsoort {coverType.Name} is aangepast";
                }
                catch
                {
                    ViewBag.Error = "Er is een probleem met de database!";
                    return View(coverType);
                }
                return RedirectToAction("Index");
            }
            return View(coverType);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _coverTypeRepository.GetFirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public IActionResult ConfirmDelete(CoverType coverType)
        {
            _coverTypeRepository.Remove(coverType);
            try
            {
                _unitOfWork.Save();
                TempData["result"] = $"Kaftsoort {coverType.Name} is verwijderd";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Er is een probleem met de database!";
                return View("Delete", coverType);
            }
            return RedirectToAction("Index");
        }


    }
}
