

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class employeeModel
    {
        public int employeeID { get; set; }
        public string farmerName { get; set; } 
        public DateTime dateAdded { get; set; } 
        public ICollection<farmerModel> Product { get; set; }
    }
}
