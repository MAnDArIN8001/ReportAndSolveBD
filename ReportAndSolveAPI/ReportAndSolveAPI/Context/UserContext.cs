using Microsoft.EntityFrameworkCore;
using ReportAndSolveAPI.Models;
using ReportAndSolveAPI.Models.DTO.User;
using ReportAndSolveAPI.Security;
using ReportAndSolveAPI.Services;

namespace ReportAndSolveAPI.Context
{
    public class UserContext : DbContext, IUserServices
    {
        private string _connectionString;

        private DbSet<UserEntity> _users { get; }

        public UserContext(DbContextOptions<UserContext> context, IConfiguration configuration) : base(context)
        {
            _connectionString = configuration.GetConnectionString("AdminConnection");

            _users = Set<UserEntity>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_connectionString);

        public async Task<ServiceResponse<List<GetUserDTO>>> GetAllUsers()
        {
            var response = new ServiceResponse<List<GetUserDTO>>();

            try
            {
                var usersFromDb = _users.FromSqlRaw("select * from getallusers()").ToList();

                if (usersFromDb is null || usersFromDb.Count == 0)
                {
                    response = ConfigureResponse<List<GetUserDTO>>(null, true, "There's no users");

                    return response;
                }

                List<GetUserDTO> usersTable = usersFromDb.Select(user => new GetUserDTO() 
                {
                    Id = user.Id,
                    RoleId = user.RoleId,
                    Name = user.Name,
                    Email = user.Email
                }).ToList();


                response = ConfigureResponse<List<GetUserDTO>>(usersTable, true, "Ok");
            }
            catch (Exception exception)
            {
                response = ConfigureResponse<List<GetUserDTO>>(null, false, exception.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetUserDTO>>> GetAllUsersWithRole(int roleId)
        {
            var response = new ServiceResponse<List<GetUserDTO>>();

            try
            {
                var usersWithRoleFromDb = _users.FromSqlRaw($"select * from getalluserswithrole({roleId})").ToList();

                if (usersWithRoleFromDb is null || usersWithRoleFromDb.Count == 0)
                {
                    response = ConfigureResponse<List<GetUserDTO>>(null, true, "There's no users with those role");

                    return response;
                }

                List<GetUserDTO> usersTable = usersWithRoleFromDb.Select(user => new GetUserDTO() 
                { 
                    Id = user.Id,
                    RoleId = user.RoleId,
                    Name = user.Name,
                    Email = user.Email
                }).ToList();

                response = ConfigureResponse<List<GetUserDTO>>(usersTable, true, "OK");
            } 
            catch (Exception exception)
            {
                response = ConfigureResponse<List<GetUserDTO>>(null, false, exception.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<GetUserDTO>> GetUser(int id)
        {
            var response = new ServiceResponse<GetUserDTO>();

            try
            {
                var userFromDb = _users.FromSqlRaw($"select * from getuserbyid({id})").ToList();

                if (userFromDb is null || userFromDb.Count == 0)
                {
                    response = ConfigureResponse<GetUserDTO>(null, true, "there's no user with those id");

                    return response;
                }

                var user = new GetUserDTO() 
                {
                    Id = userFromDb[0].Id,
                    RoleId = userFromDb[0].RoleId,
                    Name = userFromDb[0].Name,
                    Email = userFromDb[0].Email
                };

                response = ConfigureResponse<GetUserDTO>(user, true, "OK");
            }
            catch (Exception exception)
            {
                response = ConfigureResponse<GetUserDTO>(null, false, exception.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> CreateUser(AddUserDTO newUser)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                await Database.ExecuteSqlRawAsync($"CALL createnewuser({newUser.RoleId}, '{newUser.Name}', '{newUser.Email}', '{GeneratePasswordHash(newUser.Password)}')");

                response = ConfigureResponse(true, true, "OK");
            }
            catch (Exception exception)
            {
                response = ConfigureResponse(false, false, exception.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateUser(UpdateUserDTO updatedUser)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                await Database.ExecuteSqlRawAsync($"CALL updateuser({updatedUser.Id}, {updatedUser.RoleId}, '{updatedUser.Name}', '{updatedUser.Email}', '{GeneratePasswordHash(updatedUser.Password)}')");

                response = ConfigureResponse(true, true, "OK");
            }
            catch (Exception exception)
            {
                response = ConfigureResponse(false, false, exception.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteUser(int id)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                await Database.ExecuteSqlRawAsync($"CALL deleteuser({id})");

                response = ConfigureResponse(true, true, "OK");
            }
            catch (Exception exception)
            {
                response = ConfigureResponse(false, false, exception.Message);
            }

            return response;
        }

        private string GeneratePasswordHash(string password)
        {
            IHashGenerator hashGenerator = new Sha512HashGenerator();

            return hashGenerator.IncryptData(password);
        }

        private ServiceResponse<T> ConfigureResponse<T>(T data, bool sucses, string message) => new ServiceResponse<T> { Data = data, Succes = sucses, Message = message };
    }
}
