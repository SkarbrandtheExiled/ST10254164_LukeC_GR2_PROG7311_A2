using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class employeeModel
    {
        public int productID { get; set; }
        public int farmerID { get; set; }
        public required string farmerName { get; set; } 
        public DateTime dateAdded { get; set; } 
    }
}
