using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportAndSolveAPI.Models;
using ReportAndSolveAPI.Models.DTO.Status;
using ReportAndSolveAPI.Services;

namespace ReportAndSolveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusHistoryController : ControllerBase
    {
        private IStatusServices _statusService;

        public StatusHistoryController(IStatusServices statusService)
        {
            _statusService = statusService;
        }

        [HttpGet("/report/{reportId}/status")]
        public async Task<ActionResult<ServiceResponse<List<GetStatusDTO>>>> GetStatuses(int reportId)
        {
            ServiceResponse<List<GetStatusDTO>> response = await _statusService.GetReportStatuses(reportId);

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpPatch("/status/add")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddStatus(UpdateStatusDTO newStatus)
        {
            ServiceResponse<bool> response = await _statusService.UpdateStatus(newStatus);

            MailSender.SendTableChangedMesssage("Statushistory", "update");

            return response.Succes ? Ok(response) : NotFound(response);
        }
    }
}
