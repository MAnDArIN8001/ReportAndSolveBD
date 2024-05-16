using ReportAndSolveAPI.Models;
using ReportAndSolveAPI.Models.DTO.Comment;

namespace ReportAndSolveAPI.Services
{
    public interface ICommentServices
    {
        public Task<ServiceResponse<List<GetCommentDTO>>> GetAllCommentsToReport(int reportId);

        public Task<ServiceResponse<GetCommentDTO>> GetCommennt(int commentId);

        public Task<ServiceResponse<bool>> CreateComment(AddCommentDTO newComment);
        public Task<ServiceResponse<bool>> DeleteComment(int commentId);
    }
}
