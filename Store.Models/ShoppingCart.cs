using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Range(1,100,ErrorMessage ="Please Enter a value between 1 and 100")]
        public int Count { get; set; }  

        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        [ValidateNever]
        public AppUser AppUser { get; set; }
    }
}
