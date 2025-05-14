using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class productModel
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(100)]
        public string productName { get; set; }

        [Required(ErrorMessage = "Product creation date is required")]
        [DataType(DataType.Date)]
        public DateTime productCreationDate { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [MaxLength(50)]
        public string Category { get; set; }

        [Required(ErrorMessage = "Date added is required")]
        [DataType(DataType.Date)]
        public DateTime dateAdded { get; set; }

        [Required(ErrorMessage = "Farmer name is required")]
        [MaxLength(50)]
        public string farmerName { get; set; }

        // Foreign Key
        [ForeignKey("Farmer")] 
        public int FarmerId { get; set; }
        public farmerModel Farmer { get; set; }
    }
}
