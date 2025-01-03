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
   private readonly IWebHostEnvironment _hostEnvironment;

   public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
   {
      _unitOfWork = unitOfWork;
      _hostEnvironment = hostEnvironment;
   }
   public IActionResult Index()
   {
      var productList = _unitOfWork.Product.GetAll(includeProperties:"Category");
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
   public IActionResult Upsert(ProductVM productVM, IFormFile? file)
   {
      if (ModelState.IsValid)
      {
         string webRootPath = _hostEnvironment.WebRootPath;
         if(file != null)
         {
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(webRootPath, @"images\products");
            var extension = Path.GetExtension(file.FileName);
            if (productVM.Product.ImageURL != null)
            {
               var oldImagePath = Path.Combine(webRootPath, productVM.Product.ImageURL.TrimStart('\\'));
               if (System.IO.File.Exists(oldImagePath))
               {
                  System.IO.File.Delete(oldImagePath);
               }
            }
            using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
               file.CopyTo(fileStream);
            }
            productVM.Product.ImageURL = @"\images\products\" + fileName + extension;
         }

         if(productVM.Product.Id == 0)
         {
            _unitOfWork.Product.Add(productVM.Product);
         }
         else
         {
            _unitOfWork.Product.Update(productVM.Product);
         }
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

   //public IActionResult Delete(int? id)
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

   //[HttpPost, ActionName("Delete")]
   //public IActionResult DeletePOST(int? id)
   //{
   //   var product = _unitOfWork.Product.Get(u => u.Id == id);
   //   if (product == null)
   //   {
   //      return NotFound();
   //   }
   //   _unitOfWork.Product.Remove(product);
   //   _unitOfWork.Save();
   //   TempData["success"] = $"Category {product.Title} deleted successfully";
   //   return RedirectToAction("Index");
   //}

   #region API CALLS
   [HttpGet]
   public IActionResult GetAll()
   {
      var productList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
      return Json(new { data = productList });
   }

   [HttpDelete]
   public IActionResult Delete(int id)
   {
      var product = _unitOfWork.Product.Get(u => u.Id == id);
      if (product == null)
      {
         return Json(new { success = false, message = "Error while deleting" });
      }
      if (product.ImageURL != null)
      {
         var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, product.ImageURL.TrimStart('\\'));
         if (System.IO.File.Exists(oldImagePath))
         {
            System.IO.File.Delete(oldImagePath);
         }
      }
      _unitOfWork.Product.Remove(product);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Delete successful" });
   }


   #endregion
}
