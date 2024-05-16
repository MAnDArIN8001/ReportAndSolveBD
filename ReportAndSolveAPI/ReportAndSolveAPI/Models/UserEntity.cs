using System.ComponentModel.DataAnnotations.Schema;

namespace ReportAndSolveAPI.Models
{
    [Table("users")]
    public class UserEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("role")]
        public int RoleId { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("mail")]
        public string Email { get; set; } = string.Empty;
        [Column("password")]
        public string Password { get; set; } = string.Empty;
    }
}
