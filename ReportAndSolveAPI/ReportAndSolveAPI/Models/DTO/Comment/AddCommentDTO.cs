namespace ReportAndSolveAPI.Models.DTO.Comment
{
    public class AddCommentDTO
    {
        public int AuthorId { get; set; }
        public int ReportId { get; set; }

        public string Text { get; set; } = string.Empty;
    }
}
