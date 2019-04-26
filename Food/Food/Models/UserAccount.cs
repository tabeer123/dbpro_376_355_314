using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Food.Models
{
    public class UserAccount
    {
        [Required(ErrorMessage = "Please Fill name")]
        [Display(Name = "First_Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string First_Name { get; set; }
        [Required(ErrorMessage = "Please Fill name")]
        [Display(Name = "Last_Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Last_Name { get; set; }
        [Required(ErrorMessage = "Please Fill Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please Fill Cell No")]
        [Display(Name = "Cell_No")]
        public int Cell_No { get; set; }
        [EmailAddress(ErrorMessage = "Please enter a vaild email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Fill Disicriminator")]
        [Display(Name = "Discriminator")]
        public string Discriminator { get; set; }
        public string LoginErrorMessage { get; set; }
    }
}