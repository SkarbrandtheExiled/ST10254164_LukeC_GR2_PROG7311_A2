using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.employeeRepository;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.farmerRepository;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.farmerServices;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Services.FarmerServices
{
    public class FarmerServices : IFarmerServices
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IEmployeeRepository _userRepository;
        private readonly ILogger<FarmerServices> _logger;

        public FarmerServices(
            IFarmerRepository farmerRepository,
            IEmployeeRepository userRepository,
            ILogger<FarmerServices> logger)
        {
            _farmerRepository = farmerRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<farmerModel>> GetAllFarmersAsync()
        {
            return await _farmerRepository.GetAllFarmersAsync();
        }


        public async Task<farmerModel?> GetFarmerByUserIdAsync(int userId)
        {
            return await _farmerRepository.GetFarmerByUserIdAsync(userId);
        }

        public async Task<(bool Success, string? ErrorMessage)> CreateFarmerWithUserAsync(addFarmerModel model)
        {
            // Check if username already exists
            var existingUser = await _userRepository.GetUserByUsernameAsync(model.Username);
            if (existingUser != null)
            {
                return (false, "Username already exists. Please choose a different username.");
            }

            var newUser = new employeeModel
            {
                Username = model.Username,
                PasswordHash = model.Password,
                Role = "Farmer"
            };

            try
            {
                await _userRepository.AddUserAsync(newUser);

                var newFarmer = new farmerModel
                {
                    Name = model.FarmerName,
                    ContactDetails = model.ContactDetails,
                    UserId = newUser.Id
                };
                await _farmerRepository.AddFarmerAsync(newFarmer);

                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new farmer and user account for username {Username}", model.Username);
                return (false, "An unexpected error occurred while creating the account.");
            }
        }
    }
}
