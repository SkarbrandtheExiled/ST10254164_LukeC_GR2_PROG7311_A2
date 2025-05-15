using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class productModel //Product
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        public DateTime AddedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int FarmerId { get; set; } // Foreign Key
        [ForeignKey("FarmerId")]
        public virtual farmerModel Farmer { get; set; }
    }
}
