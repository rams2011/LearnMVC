using System.Diagnostics;
using Learn.DataAccess.Repository;
using Learn.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnWeb.Areas.Customer.Controllers
{
   [Area("Customer")]
   public class HomeController : Controller
   {
      private readonly ILogger<HomeController> _logger;
      private readonly IUnitOfWork _unitOfWork;

      public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
      {
         _logger = logger;
         this._unitOfWork = unitOfWork;
      }

      public IActionResult Index()
      {
         IEnumerable<Product> categoryList = _unitOfWork.Product.GetAll(includeProperties: "Category");
         return View(categoryList);
      }

      public IActionResult Details(int id)
      {
         var product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties:"Category");
         return View(product);
      }

      public IActionResult Privacy()
      {
         return View();
      }

      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
      public IActionResult Error()
      {
         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
      }
   }
}
