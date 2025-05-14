using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.employeeServices
{
    public interface IEmployeeServices
    {
        Task<bool> ValidateEmployeeCredentialsAsync(string username, string password);
        Task<employeeModel?> GetEmployeeByNameAsync(string name);
        Task<employeeModel?> GetEmployeeByCredentialsAsync(string username, string password);
    }
}
