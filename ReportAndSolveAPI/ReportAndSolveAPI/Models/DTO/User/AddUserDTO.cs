namespace ReportAndSolveAPI.Models.DTO.User
{
    public class AddUserDTO
    {
        public int RoleId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
