using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Data;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Repositories.employeeRepository
{
    //------------------------------START OF FILE--------------------------------//
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly applicationDBContext _context;

        public EmployeeRepository(applicationDBContext context)
        {
            _context = context;
        }

        public async Task<employeeModel?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<employeeModel?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task AddUserAsync(employeeModel user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

    }
}
//------------------------------END OF FILE--------------------------------//