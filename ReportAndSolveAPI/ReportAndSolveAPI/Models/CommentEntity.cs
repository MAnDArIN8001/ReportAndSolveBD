using System.ComponentModel.DataAnnotations.Schema;

namespace ReportAndSolveAPI.Models
{
    [Table("comment")]
    public class CommentEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("author")]
        public int AuthorId { get; set; }
        [Column("report")]
        public int ReportId { get; set; }

        [Column("text")]
        public string Text { get; set; } = string.Empty;
    }
}
