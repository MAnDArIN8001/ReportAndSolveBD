using ReportAndSolveAPI.Models;
using ReportAndSolveAPI.Models.DTO.Report;

namespace ReportAndSolveAPI.Services
{
    public interface IReportServices
    {
        public Task<ServiceResponse<List<GetReportDTO>>> GetAllReports();
        public Task<ServiceResponse<List<GetReportDTO>>> GetReportsByAuthorId(int id);

        public Task<ServiceResponse<GetReportDTO>> GetReportById(int id);

        public Task<ServiceResponse<bool>> CreateReport(AddReportDTO newReport);
        public Task<ServiceResponse<bool>> UpdateReport(UpdateReportDTO updateReport);
        public Task<ServiceResponse<bool>> DeleteReport(int reportId);
    }
}
