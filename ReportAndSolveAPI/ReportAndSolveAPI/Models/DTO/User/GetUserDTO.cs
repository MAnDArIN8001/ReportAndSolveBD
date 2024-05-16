﻿namespace ReportAndSolveAPI.Models.DTO.User
{
    public class GetUserDTO
    {
        public int Id { get; set; }
        public int RoleId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
