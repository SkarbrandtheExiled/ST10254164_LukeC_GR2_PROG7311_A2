using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.employeeRepository
{
    public interface IEmployeeRepository
    {
        Task<employeeModel?> GetUserByUsernameAsync(string username);
        Task<employeeModel?> GetUserByIdAsync(int userId);
        Task AddUserAsync(employeeModel user);

    }
}
