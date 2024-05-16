using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportAndSolveAPI.Models;
using ReportAndSolveAPI.Models.DTO.User;
using ReportAndSolveAPI.Services;

namespace ReportAndSolveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServices _userServices;

        public UserController(IUserServices userServices) 
        {
            _userServices = userServices;
        }

        [HttpGet("/users")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDTO>>>> GetAllUsers()
        {
            ServiceResponse<List<GetUserDTO>> response = await _userServices.GetAllUsers();

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpGet("/users/{roleId}")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDTO>>>> GetUsersWithRole(int roleId)
        {
            ServiceResponse<List<GetUserDTO>> response = await _userServices.GetAllUsersWithRole(roleId);

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpGet("/user/{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDTO>>> GetUser(int id)
        {
            ServiceResponse<GetUserDTO> response = await _userServices.GetUser(id);

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpPost("/user/create")]
        public async Task<ActionResult<ServiceResponse<bool>>> CreateUser(AddUserDTO newUser)
        {
            ServiceResponse<bool> response = await _userServices.CreateUser(newUser);

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpPatch("/user/update")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateUser(UpdateUserDTO updatedUser)
        {
            ServiceResponse<bool> response = await _userServices.UpdateUser(updatedUser);

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpDelete("/user/{id}")] 
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteUser(int id)
        {
            ServiceResponse<bool> response = await _userServices.DeleteUser(id);

            return response.Succes ? Ok(response) : NotFound(response);
        }
    }
}
