using Learn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.DataAccess.Repository
{
   public class ProductRepository : Repository<Product>, IProductRepository
   {
      private ApplicationDbContext _db;

      public ProductRepository(ApplicationDbContext db) : base(db)
      {
         _db = db;
      }
      //public void Save()
      //{
      //   _db.SaveChanges();
      //}
      public void Update(Product product)
      {
         var productFromDb = _db.Products.FirstOrDefault(s => s.Id == product.Id);
         if (productFromDb != null)
         {
            productFromDb.Title = product.Title;
            productFromDb.ISBN = product.ISBN;
            productFromDb.Author = product.Author;
            productFromDb.Description = product.Description;
            productFromDb.ListPrice = product.ListPrice;
            productFromDb.Price = product.Price;
            productFromDb.Price50 = product.Price50;
            productFromDb.Price100 = product.Price100;
            productFromDb.CategoryId = product.CategoryId;
            if (product.ImageURL != null)
            {
               productFromDb.ImageURL = product.ImageURL;
            }
         }
         //_db.Products.Update(product);
      }
   }
}
