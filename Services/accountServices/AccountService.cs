using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.employeeRepository;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.accountServices
{
    public class AccountService : IAccountService
    {
        private readonly IEmployeeRepository _userRepository;


        public AccountService(IEmployeeRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<employeeModel?> ValidateCredentialsAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return null; // User not found
            }


            if (user.Password == password)
            {
                return user; // Credentials are valid
            }

            return null; // Password incorrect
        }
    }
}
