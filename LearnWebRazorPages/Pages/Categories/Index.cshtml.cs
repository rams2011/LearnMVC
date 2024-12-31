using LearnWebRazorPages.Data;
using LearnWebRazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnWebRazorPages.Pages.Catogories
{
   [BindProperties]
   public class IndexModel : PageModel
   {
      private readonly ApplicationDbContext _dbContext;
      public List<Category> Categories { get; set; }

      public IndexModel(ApplicationDbContext dbContext)
      {
         this._dbContext = dbContext;
      }
      public void OnGet()
      {
         Categories = _dbContext.Categories.ToList();
      }
   }
}
