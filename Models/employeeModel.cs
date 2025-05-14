using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class employeeModel
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee name is required")]
        [MaxLength(50)]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}
