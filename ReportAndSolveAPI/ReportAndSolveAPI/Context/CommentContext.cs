using Microsoft.EntityFrameworkCore;
using ReportAndSolveAPI.Models;
using ReportAndSolveAPI.Models.DTO.Comment;
using ReportAndSolveAPI.Models.DTO.User;
using ReportAndSolveAPI.Security;
using ReportAndSolveAPI.Services;

namespace ReportAndSolveAPI.Context
{
    public class CommentContext : DbContext, ICommentServices
    {
        private string _connectionString;

        private DbSet<CommentEntity> _comments { get; }

        public CommentContext(DbContextOptions<CommentContext> context, IConfiguration configuration) : base(context)
        {
            _connectionString = configuration.GetConnectionString("AdminConnection");

            _comments = Set<CommentEntity>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_connectionString);

        public async Task<ServiceResponse<List<GetCommentDTO>>> GetAllCommentsToReport(int reportId)
        {
            var response = new ServiceResponse<List<GetCommentDTO>>();

            try
            {
                var commentsFromDb = _comments.FromSqlRaw($"select * from getallcommentstoreport({reportId})")?.ToList();

                if(commentsFromDb is null || commentsFromDb.Count == 0)
                {
                    response = ConfigureResponse<List<GetCommentDTO>>(null, true, "Sorry, theres no comments under this post");

                    return response;
                }

                List<GetCommentDTO> commentsList = commentsFromDb.Select(comment => new GetCommentDTO()
                {
                    Id = comment.Id,
                    AuthorId = comment.AuthorId,
                    ReportId = comment.ReportId,
                    Text = comment.Text
                }).ToList();

                response = ConfigureResponse(commentsList, true, "OK");
            }
            catch (Exception exception)
            {
                response = ConfigureResponse<List<GetCommentDTO>>(null, false, exception.Message);
            }

            return response; 
        }

        public async Task<ServiceResponse<GetCommentDTO>> GetCommennt(int commentId)
        {
            var response = new ServiceResponse<GetCommentDTO>();

            try
            {
                var commentFromBd = _comments.FromSqlRaw($"select * from getcomment({commentId})")?.ToList();

                if(commentFromBd is null || commentFromBd?.Count == 0)
                {
                    response = ConfigureResponse<GetCommentDTO>(null, true, "There's no comments with those id");

                    return response;
                }

                GetCommentDTO comment = commentFromBd.Select(comment => new GetCommentDTO()
                {
                    Id = comment.Id,
                    AuthorId = comment.AuthorId,
                    ReportId = comment.ReportId,
                    Text = comment.Text
                }).ToArray().First();

                response = ConfigureResponse<GetCommentDTO>(comment, true, "OK");
            }
            catch (Exception exception)
            {
                response = ConfigureResponse<GetCommentDTO>(null, false, exception.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> CreateComment(AddCommentDTO newComment)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                await Database.ExecuteSqlRawAsync($"CALL createcomment({newComment.AuthorId}, {newComment.ReportId}, '{newComment.Text}')");

                response = ConfigureResponse(true, true, "OK");
            } 
            catch (Exception exception)
            {
                response = ConfigureResponse(false, false, exception.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteComment(int commentId)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                await Database.ExecuteSqlRawAsync($"CALL deletecomment({commentId})");

                response = ConfigureResponse(true, true, "OK");
            }
            catch (Exception exception)
            {
                response = ConfigureResponse(false, false, exception.Message);
            }

            return response;
        }

        private ServiceResponse<T> ConfigureResponse<T>(T data, bool sucses, string message) => new ServiceResponse<T> { Data = data, Succes = sucses, Message = message };
    }
}
