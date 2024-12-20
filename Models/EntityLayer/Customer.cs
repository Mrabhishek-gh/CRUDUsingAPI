using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApiAssignment1.Models.EntityLayer
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(25, ErrorMessage = "Username should not exceed 25 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Gender must contain only letters.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; }


    }
}