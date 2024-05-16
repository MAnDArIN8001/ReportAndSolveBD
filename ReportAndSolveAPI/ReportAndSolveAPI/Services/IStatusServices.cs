using ReportAndSolveAPI.Models;
using ReportAndSolveAPI.Models.DTO.Status;

namespace ReportAndSolveAPI.Services
{
    public interface IStatusServices
    {
        public Task<ServiceResponse<List<GetStatusDTO>>> GetReportStatuses(int reportId);

        public Task<ServiceResponse<bool>> UpdateStatus(UpdateStatusDTO newStatus);
    }
}
