using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class farmerModel
    {
        [Key]
        public int FarmerId { get; set; }

        [Required(ErrorMessage = "Farmer name is required")]
        [MaxLength(50)] // Add maximum length for safety
        public string FarmerName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(100)] // Consider using a more reasonable length
        public string Password { get; set; }

        //  one-to-many relationship with the ProductModel
        public ICollection<productModel> products { get; set; }
    }
}
