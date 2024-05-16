namespace ReportAndSolveAPI.Models.DTO.User
{
    public class UpdateUserDTO
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        
        public string Name {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
