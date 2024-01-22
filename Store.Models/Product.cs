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
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string TypeProduct { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name ="Price Products : ")]
        public double price { get; set; }
        public DateTime Release_Date { get; set; }
        public int CategortId { get; set; }
        [ForeignKey("CategortId")]
        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }

    }
}
