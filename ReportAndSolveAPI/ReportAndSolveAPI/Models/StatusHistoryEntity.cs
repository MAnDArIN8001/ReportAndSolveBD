using System.ComponentModel.DataAnnotations.Schema;

namespace ReportAndSolveAPI.Models
{
    [Table("statushistory")]
    public class StatusHistoryEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("report")]
        public int ReportId { get; set; }

        [Column("statuses")]
        public string[] statuses { get; set; }
    }
}
