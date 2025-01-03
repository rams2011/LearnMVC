using Learn.DataAccess.Repository;
using Learn.Models;
using Learn.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearnWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
   private readonly IUnitOfWork _unitOfWork;

   public ProductController(IUnitOfWork unitOfWork)
   {
      _unitOfWork = unitOfWork;
   }
   public IActionResult Index()
   {
      var productList = _unitOfWork.Product.GetAll();
      return View(productList);
   }

   //public IActionResult Create()
   //{
   //   var categoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
   //   {
   //      Text = u.Name,
   //      Value = u.Id.ToString()
   //   });
   //   //ViewBag.CategoryList = categoryList;
   //   ProductVM productVM = new()
   //   {
   //      Product = new Product(),
   //      CategoryList = categoryList
   //   };
   //   return View(productVM);
   //}
   public IActionResult Upsert(int? id)
   {
      var categoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
      {
         Text = u.Name,
         Value = u.Id.ToString()
      });
      //ViewBag.CategoryList = categoryList;
      ProductVM productVM = new()
      {
         Product = new Product(),
         CategoryList = categoryList
      };
      if(id != null && id != 0)
      {
         productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
      }
      return View(productVM);
   }

   [HttpPost]
   public IActionResult Create(ProductVM productVM, IFormFile? file)
   {
      if (ModelState.IsValid)
      {
         _unitOfWork.Product.Add(productVM.Product);
         _unitOfWork.Save();
         TempData["success"] = $"Product {productVM.Product.Title} created successfully";
         return RedirectToAction("Index");
      }
      else
      {
         productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
         {
            Text = u.Name,
            Value = u.Id.ToString()
         });
         return View(productVM);
      }
   }

   //public IActionResult Edit(int? id)
   //{
   //   if (id == null || id == 0)
   //   {
   //      return NotFound();
   //   }
   //   var category = _unitOfWork.Product.Get(u => u.Id == id);
   //   if (category == null)
   //   {
   //      return NotFound();
   //   }
   //   return View(category);
   //}

   //[HttpPost]
   //public IActionResult Edit(Product product)
   //{
   //   if (ModelState.IsValid)
   //   {
   //      _unitOfWork.Product.Update(product);
   //      _unitOfWork.Save();
   //      TempData["success"] = $"Product {product.Title} edited successfully";
   //      return RedirectToAction("Index");
   //   }
   //   return View();
   //}

   public IActionResult Delete(int? id)
   {
      if (id == null || id == 0)
      {
         return NotFound();
      }
      var category = _unitOfWork.Product.Get(u => u.Id == id);
      if (category == null)
      {
         return NotFound();
      }
      return View(category);
   }

   [HttpPost, ActionName("Delete")]
   public IActionResult DeletePOST(int? id)
   {
      var product = _unitOfWork.Product.Get(u => u.Id == id);
      if (product == null)
      {
         return NotFound();
      }
      _unitOfWork.Product.Remove(product);
      _unitOfWork.Save();
      TempData["success"] = $"Category {product.Title} deleted successfully";
      return RedirectToAction("Index");
   }
}
