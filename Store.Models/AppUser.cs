using Microsoft.AspNetCore.Identity;
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
    public class AppUser:IdentityUser
    {
        [Required]
        public string name { get; set; }
        public string PhoneNumber {  get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }

        [ForeignKey("Company")]
        [ValidateNever]
        public int? CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
