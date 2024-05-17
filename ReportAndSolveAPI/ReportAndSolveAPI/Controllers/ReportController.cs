using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportAndSolveAPI.Models;
using ReportAndSolveAPI.Models.DTO.Report;
using ReportAndSolveAPI.Services;

namespace ReportAndSolveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IReportServices _reportServices;

        public ReportController(IReportServices reportServices)
        {
            _reportServices = reportServices;
        }

        [HttpGet("/reports")]
        public async Task<ActionResult<ServiceResponse<List<GetReportDTO>>>> GetAllReports()
        {
            ServiceResponse<List<GetReportDTO>> response = await _reportServices.GetAllReports();

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpGet("/reports/author/{authorId}")]
        public async Task<ActionResult<ServiceResponse<List<GetReportDTO>>>> GetAllReportWithAuthor(int authorId)
        {
            ServiceResponse<List<GetReportDTO>> response = await _reportServices.GetReportsByAuthorId(authorId);

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpGet("/report/{id}")]
        public async Task<ActionResult<ServiceResponse<GetReportDTO>>> GetReportById(int id)
        {
            ServiceResponse<GetReportDTO> response = await _reportServices.GetReportById(id);

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpPost("/report/create")]
        public async Task<ActionResult<ServiceResponse<bool>>> CreateReport(AddReportDTO newReport)
        {
            ServiceResponse<bool> response = await _reportServices.CreateReport(newReport);

            MailSender.SendTableChangedMesssage("Report", "insert");

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpPatch("/report/update")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateReport(UpdateReportDTO updateReport)
        {
            ServiceResponse<bool> response = await _reportServices.UpdateReport(updateReport);

            MailSender.SendTableChangedMesssage("Users", "update");

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpDelete("/report/{id}/delete")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteReport(int id)
        {
            ServiceResponse<bool> response = await _reportServices.DeleteReport(id);

            MailSender.SendTableChangedMesssage("Report", "delete");

            return response.Succes ? Ok(response) : NotFound(response);
        }
    }
}
