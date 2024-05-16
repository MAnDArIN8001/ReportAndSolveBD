using System.ComponentModel.DataAnnotations.Schema;

namespace ReportAndSolveAPI.Models
{
    [Table("report")]
    public class ReportEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("author")]
        public int AuthorId { get; set; }

        [Column("title")]
        public string? Title { get; set; } = string.Empty;
        [Column("text")]
        public string? Text { get; set; } = string.Empty;
    }
}
