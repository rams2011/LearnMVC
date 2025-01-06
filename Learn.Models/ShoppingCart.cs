﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Models
{
   public class ShoppingCart
   {
      public int Id { get; set; }
      public int ProductId { get; set; }
      [ValidateNever]
      [ForeignKey("ProductId")]
      public Product Product { get; set; }
      [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
      public int Quantity { get; set; }

      public string ApplicationUserId { get; set; }

      [ValidateNever]
      [ForeignKey("ApplicationUserId")]
      public ApplicationUser ApplicationUser { get; set; }
   }
}
