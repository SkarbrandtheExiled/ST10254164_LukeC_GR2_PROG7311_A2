

using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class employeeModel 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        public virtual farmerModel? FarmerProfile { get; set; }
    }
}
