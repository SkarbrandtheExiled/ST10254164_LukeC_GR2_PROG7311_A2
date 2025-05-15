using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class farmerModel //Farmer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Email { get; set; }

        // Foreign Key to User
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual employeeModel employeeModel { get; set; }

        // Navigation property: A farmer can have many products
        public virtual ICollection<productModel> Products { get; set; } = new List<productModel>();
    }
}
