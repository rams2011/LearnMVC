using Learn.DataAccess;
using Learn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnWeb.Controllers;

public class CategoryController : Controller
{
   private readonly ApplicationDbContext _dbContext;

   public CategoryController(ApplicationDbContext dbContext)
   {
      _dbContext = dbContext;
   }
   public IActionResult Index()
   {
      var objCategoryList = _dbContext.Categories.ToList();
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
         _dbContext.Categories.Add(obj);
         _dbContext.SaveChanges();
         TempData["success"] = $"Category {obj.Name} created successfully";
         return RedirectToAction("Index");
      }
      return View();
   }

   public IActionResult Edit(int? id)
   {
      if(id == null || id == 0)
      {
         return NotFound();
      }
      //var category = _dbContext.Categories.Find(id);
      var category = _dbContext.Categories.FirstOrDefault(u => u.Id == id);
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
         _dbContext.Categories.Update(obj);
         _dbContext.SaveChanges();
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
      var category = _dbContext.Categories.FirstOrDefault(u => u.Id == id);
      if (category == null)
      {
         return NotFound();
      }
      return View(category);
   }

   [HttpPost, ActionName("Delete")]
   public IActionResult DeletePOST(int? id)
   {
      var category = _dbContext.Categories.Find(id);
      if(category == null) {
         return NotFound();
      }
      _dbContext.Categories.Remove(category);
      _dbContext.SaveChanges();
      TempData["success"] = $"Category {category.Name} deleted successfully";
      return RedirectToAction("Index");
   }
}
