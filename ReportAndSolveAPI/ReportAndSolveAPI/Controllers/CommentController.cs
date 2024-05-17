using Microsoft.AspNetCore.Mvc;
using ReportAndSolveAPI.Models;
using ReportAndSolveAPI.Models.DTO.Comment;
using ReportAndSolveAPI.Services;

namespace ReportAndSolveAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentServices _commentService;

        public CommentController(ICommentServices commentServices)
        {
            _commentService = commentServices;
        }

        [HttpGet("/report/{reportId}/comments")]
        public async Task<ActionResult<ServiceResponse<List<GetCommentDTO>>>> GetComments(int reportId)
        {
            ServiceResponse<List<GetCommentDTO>> response = await _commentService.GetAllCommentsToReport(reportId);

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpGet("/comment/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCommentDTO>>> GetComment(int id)
        {
            ServiceResponse<GetCommentDTO> response = await _commentService.GetCommennt(id);

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpPost("/comment/create")]
        public async Task<ActionResult<ServiceResponse<bool>>> CreateComment(AddCommentDTO newComment)
        {
            ServiceResponse<bool> response = await _commentService.CreateComment(newComment);

            MailSender.SendTableChangedMesssage("Comment", "create");

            return response.Succes ? Ok(response) : NotFound(response);
        }

        [HttpDelete("/comment/{id}/delete")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteComment(int id)
        {
            ServiceResponse<bool> response = await _commentService.DeleteComment(id);

            MailSender.SendTableChangedMesssage("Comment", "delete");

            return response;
        }
    }
}
