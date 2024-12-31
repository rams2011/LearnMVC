using LearnWebRazorPages.Data;
using LearnWebRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnWebRazorPages.Pages.Categories;

[BindProperties]
 public class EditModel : PageModel
 {
   private readonly ApplicationDbContext _dbContext;

   public Category? Category { get; set; }
   public EditModel(ApplicationDbContext dbContext)
   {
      this._dbContext = dbContext;
   }
   public void OnGet(int? id)
   {
      if (id != null || id != 0)
      {
         Category = _dbContext.Categories.FirstOrDefault(u => u.Id == id);
      }
   }

   public IActionResult OnPost()
   {
      if (ModelState.IsValid)
      {
         _dbContext.Categories.Update(Category);
         _dbContext.SaveChanges();
         //TempData["success"] = $"Category {Category.Name} edited successfully";
         return RedirectToPage("Index");
      }
      return Page();
   }
 }
