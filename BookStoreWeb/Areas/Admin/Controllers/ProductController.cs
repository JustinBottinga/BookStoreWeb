using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public ProductController(IUnitOfWork context, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = context;
            _productRepository = _unitOfWork.Product;
            _webHostEnviroment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(_productRepository.GetAll());
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                //catergories dropdown
                CategoryList = _unitOfWork.Catergory.GetAll().Select(
                    c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }),
                //covertypes dropdown
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                return View(productVM);
            }

            //update product
            productVM.Product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == id);
            return View(productVM);

        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRoot = _webHostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string imagesLocation = Path.Combine(wwwRoot, @"images\products");
                    string extension = Path.GetExtension(file.FileName);
                    using (var fs = new FileStream(Path.Combine(imagesLocation, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fs);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                _unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["succes"] = "Product opgeslagen";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType")
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.ISBN,
                    p.Price,
                    p.Author,
                    CategoryName = p.Category.Name, // Access the Category name directly
                    CoverTypeName = p.CoverType.Name // Access CoverType name if needed
                });

            return Json(new { data = productList });
        }

        #endregion
    }
}
