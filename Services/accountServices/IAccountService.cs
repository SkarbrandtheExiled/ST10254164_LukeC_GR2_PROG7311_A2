using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.accountServices
{
    //------------------------------START OF FILE--------------------------------//
    public interface IAccountService
    {
        Task<employeeModel?> ValidateCredentialsAsync(string username, string password); // Validates the user's credentials

    }
}
//------------------------------END OF FILE--------------------------------//