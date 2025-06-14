﻿using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{ /// This class is used to create a new farmer user in the system
    public class addFarmerModel
    {
        [Required]
        [Display(Name = "Farmer Name")]
        public string FarmerName { get; set; }

        [Display(Name = "Contact Details")]
        public string? Email { get; set; } // Optional field not really used in the system

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
