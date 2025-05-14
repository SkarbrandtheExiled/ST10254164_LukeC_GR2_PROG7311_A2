using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class employeeModel
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public string employeeName { get; set; }
        public DateTime dateAdded { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
