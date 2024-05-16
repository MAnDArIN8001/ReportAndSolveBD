using ReportAndSolveAPI.Models;
using ReportAndSolveAPI.Models.DTO.User;

namespace ReportAndSolveAPI.Services
{
    public interface IUserServices
    {
        public Task<ServiceResponse<List<GetUserDTO>>> GetAllUsers();
        public Task<ServiceResponse<List<GetUserDTO>>> GetAllUsersWithRole(int roleId);

        public Task<ServiceResponse<GetUserDTO>> GetUser(int id);

        public Task<ServiceResponse<bool>> CreateUser(AddUserDTO newUser);
        public Task<ServiceResponse<bool>> UpdateUser(UpdateUserDTO newUser);
        public Task<ServiceResponse<bool>> DeleteUser(int id);
    }
}
