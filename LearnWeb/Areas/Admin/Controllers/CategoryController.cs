using Learn.DataAccess;
using Learn.DataAccess.Repository;
using Learn.Models;
using Learn.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnWeb.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CategoryController : Controller
{
   private readonly IUnitOfWork _unitOfWork;

   //private readonly ICategoryRepository _categoryRepository;
   public CategoryController(IUnitOfWork unitOfWork)
   {
      //_categoryRepository = dbContext;
      _unitOfWork = unitOfWork;
   }
   public IActionResult Index()
   {
      var objCategoryList = _unitOfWork.Category.GetAll();
      return View(objCategoryList);
   }

   public IActionResult Create()
   {
      return View();
   }

   [HttpPost]
   public IActionResult Create(Category obj)
   {
      //if(obj.Name == obj.DisplayOrder.ToString())
      //{
      //   ModelState.AddModelError("name", "The DisplayOder cannot exactly match the Name");
      //}
      //if (obj.Name.ToLower() == "test")
      //{
      //   ModelState.AddModelError("name", "Test is an invalid value");
      //}
      if (ModelState.IsValid)
      {
         _unitOfWork.Category.Add(obj);
         _unitOfWork.Save();
         TempData["success"] = $"Category {obj.Name} created successfully";
         return RedirectToAction("Index");
      }
      return View();
   }

   public IActionResult Edit(int? id)
   {
      if (id == null || id == 0)
      {
         return NotFound();
      }
      //var category = _dbContext.Categories.Find(id);
      var category = _unitOfWork.Category.Get(u => u.Id == id);
      //var category = _dbContext.Categories.Where(u => u.Id == id).FirstOrDefault();
      if (category == null)
      {
         return NotFound();
      }
      return View(category);
   }

   [HttpPost]
   public IActionResult Edit(Category obj)
   {
      if (ModelState.IsValid)
      {
         _unitOfWork.Category.Update(obj);
         _unitOfWork.Save();
         TempData["success"] = $"Category {obj.Name} edited successfully";
         return RedirectToAction("Index");
      }
      return View();
   }

   public IActionResult Delete(int? id)
   {
      if (id == null || id == 0)
      {
         return NotFound();
      }
      var category = _unitOfWork.Category.Get(u => u.Id == id);
      if (category == null)
      {
         return NotFound();
      }
      return View(category);
   }

   [HttpPost, ActionName("Delete")]
   public IActionResult DeletePOST(int? id)
   {
      var category = _unitOfWork.Category.Get(u => u.Id == id);
      if (category == null)
      {
         return NotFound();
      }
      _unitOfWork.Category.Remove(category);
      _unitOfWork.Save();
      TempData["success"] = $"Category {category.Name} deleted successfully";
      return RedirectToAction("Index");
   }
}
