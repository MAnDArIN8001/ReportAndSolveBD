namespace ReportAndSolveAPI.Models.DTO.Comment
{
    public class GetCommentDTO
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int ReportId { get; set; }

        public string Text { get; set; } = string.Empty;
    }
}
