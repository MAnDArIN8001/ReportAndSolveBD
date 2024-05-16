using System.ComponentModel.DataAnnotations.Schema;

namespace ReportAndSolveAPI.Models
{
    [Table("role")]
    public class RoleEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
