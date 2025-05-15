using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.accountServices
{
    public interface IAccountService
    {
        Task<employeeModel?> ValidateCredentialsAsync(string username, string password);

    }
}
