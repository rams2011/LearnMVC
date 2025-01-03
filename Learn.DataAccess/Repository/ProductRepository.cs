﻿using Learn.Models;
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
         _db.Products.Update(product);
      }
   }
}
