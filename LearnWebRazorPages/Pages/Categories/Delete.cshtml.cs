using LearnWebRazorPages.Data;
using LearnWebRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnWebRazorPages.Pages.Categories;

[BindProperties]
public class DeleteModel : PageModel
{
   private readonly ApplicationDbContext _dbContext;
   public Category? Category { get; set; }

   public DeleteModel(ApplicationDbContext dbcontext)
   {
      this._dbContext = dbcontext;
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
      var category = _dbContext.Categories.Find(Category.Id);
      if (category == null)
      {
         return NotFound();
      }
      _dbContext.Categories.Remove(category);
      _dbContext.SaveChanges();
      //TempData["success"] = $"Category {category.Name} deleted successfully";
      return RedirectToPage("Index");
   }
}
