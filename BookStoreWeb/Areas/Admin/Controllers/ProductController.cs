using BookStore.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public ProductController(IUnitOfWork context)
        {
            _unitOfWork = context;
            _productRepository = _unitOfWork.Product;
        }
        public IActionResult Index()
        {
            return View(_productRepository.GetAll());
        }
    }
}
