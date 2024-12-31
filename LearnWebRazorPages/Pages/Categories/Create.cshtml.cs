using LearnWebRazorPages.Data;
using LearnWebRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnWebRazorPages.Pages.Categories
{
   [BindProperties]
    public class CreateModel : PageModel
    {
      private readonly ApplicationDbContext _dbContext;

      public Category Category { get; set; }
      public CreateModel(ApplicationDbContext dbContext)
      {
         this._dbContext = dbContext;
      }
      public void OnGet()
      {
      }

      public IActionResult OnPost()
      {
         _dbContext.Categories.Add(Category);
         _dbContext.SaveChanges();
         //TempData["success"] = $"Category {obj.Name} created successfully";
         return RedirectToPage("Index");
      }
   }
}
