using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class farmerProductModel
    {
        [Key]
        public int farmerProductID { get; set; }
        [Required]
        public string productName { get; set; }
        [Required]
        public string Category { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime productCreationDate { get; set; }
    }
}
