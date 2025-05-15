using System.ComponentModel.DataAnnotations;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Models
{
    public class loginModel
    { //------------------------------START OF FILE--------------------------------//
        [Required(ErrorMessage = "Username")] //used to create a new farmer user in the system
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
//--------------------------------------END OF FILE----------------------------------//