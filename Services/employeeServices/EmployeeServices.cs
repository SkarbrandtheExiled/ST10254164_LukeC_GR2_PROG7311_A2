using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.employeeRepository;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.employeeServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository _repo;
        public EmployeeServices(IEmployeeRepository repo) => _repo = repo;
        public async Task<bool> ValidateEmployeeCredentialsAsync(string username, string password)
        {
            var employee = await _repo.GetEmployeeByCredentialsAsync(username, password);
            return employee != null;
        }
        public async Task<employeeModel?> GetEmployeeByNameAsync(string name) => await _repo.GetByNameAsync(name);
        public async Task<employeeModel?> GetEmployeeByCredentialsAsync(string username, string password) => await _repo.GetEmployeeByCredentialsAsync(username, password);
    }
}
