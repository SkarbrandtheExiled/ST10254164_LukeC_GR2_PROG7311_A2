using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class productModel
    {
        [Key]
        public int ProductID { get; set; }

        // Farmer-entered data
        [Required]
        public string productName { get; set; }

        [Required]
        public string Category { get; set; }

        [DataType(DataType.Date)]
        public DateTime productCreationDate { get; set; }

        // Employee-entered data
        public string farmerName { get; set; }

        [DataType(DataType.Date)]
        public DateTime dateAdded { get; set; }
    }
}
