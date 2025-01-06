using Learn.DataAccess.Repository;
using Learn.Models;
using Learn.Models.ViewModels;
using Learn.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearnWeb.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = SD.Role_Admin)]
public class CompanyController : Controller
{
   private readonly IUnitOfWork _unitOfWork;

   public CompanyController(IUnitOfWork unitOfWork)
   {
      _unitOfWork = unitOfWork;
   }
   public IActionResult Index()
   {
      var CompanyList = _unitOfWork.Company.GetAll();
      return View(CompanyList);
   }

   public IActionResult Upsert(int? id)
   {
      var categoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
      {
         Text = u.Name,
         Value = u.Id.ToString()
      });
      if(id != null && id != 0)
      {
         var company = _unitOfWork.Company.Get(u => u.Id == id);
         return View(company);
      }
      else
      {
         return View(new Company());
      }
   }

   [HttpPost]
   public IActionResult Upsert(Company Company)
   {
      if (ModelState.IsValid)
      {
         if(Company.Id == 0)
         {
            _unitOfWork.Company.Add(Company);
         }
         else
         {
            _unitOfWork.Company.Update(Company);
         }
         _unitOfWork.Save();
         TempData["success"] = $"Company {Company.Name} created successfully";
         return RedirectToAction("Index");
      }
      else
      {
         return View(Company);
      }
   }

   #region API CALLS
   [HttpGet]
   public IActionResult GetAll()
   {
      var CompanyList = _unitOfWork.Company.GetAll().ToList();
      return Json(new { data = CompanyList });
   }

   [HttpDelete]
   public IActionResult Delete(int id)
   {
      var Company = _unitOfWork.Company.Get(u => u.Id == id);
      if (Company == null)
      {
         return Json(new { success = false, message = "Error while deleting" });
      }
      _unitOfWork.Company.Remove(Company);
      _unitOfWork.Save();
      return Json(new { success = true, message = "Delete successful" });
   }
   #endregion
}
