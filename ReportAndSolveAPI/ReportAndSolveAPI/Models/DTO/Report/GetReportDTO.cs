using System.Text.Json.Serialization;

namespace ReportAndSolveAPI.Models.DTO.Report
{
    public class GetReportDTO
    {
        public int Id { get; set; }
        public int Author { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
