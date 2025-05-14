using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.employeeRepository
{
    public interface IEmployeeRepository
    {
        Task<employeeModel?> GetByNameAsync(string name);
        Task<employeeModel?> GetEmployeeByCredentialsAsync(string username, string password);
    }
}
