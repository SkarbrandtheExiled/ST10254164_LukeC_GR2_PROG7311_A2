using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Data;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.employeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly applicationDBContext _context;
        public EmployeeRepository(applicationDBContext context) => _context = context;
        public async Task<employeeModel?> GetByNameAsync(string name)
            => await _context.employees.FirstOrDefaultAsync(e => e.employeeName == name);
        public async Task<employeeModel?> GetEmployeeByCredentialsAsync(string username, string password)
            => await _context.employees.FirstOrDefaultAsync(e => e.employeeName == username && e.Password == password);
    }
}
