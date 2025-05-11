using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class productModel
    {
        public int productID { get; set; }
        public string productName { get; set; }
        public string Category { get; set; }
        public DateTime productCreationDate { get; set; }
        public DateTime dateAdded { get; set; }

        public string UserId { get; set; } 
        public ApplicationUser User { get; set; }
    }
}
