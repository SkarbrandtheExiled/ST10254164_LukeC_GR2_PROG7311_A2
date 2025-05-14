using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class productModel
    {
        [Key]
        public int productID { get; set; }

        [Required]
        public string productName { get; set; }

        [Required]
        public string Category { get; set; }

        [DataType(DataType.Date)]
        public DateTime productCreationDate { get; set; }

 public int farmerID { get; set; }
        public farmerModel Farmer { get; set; }
        public string farmerName { get; set; }

        [DataType(DataType.Date)]
        public DateTime dateAdded { get; set; }
    }
}
