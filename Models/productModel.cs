using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class productModel
    {
        [Display(Name = "Product ID")] 
        public int productID { get; set; }


        [Display(Name = "Product Name")] 
        public required string name { get; set; } 


        [Display(Name = "Creation Date")] 
        [DataType(DataType.Date)] 
        public DateTime productDate { get; set; } 

        [Display(Name = "Category")] 
        public required string Category { get; set; }
    }
}
