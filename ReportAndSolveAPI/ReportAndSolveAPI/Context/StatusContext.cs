using Microsoft.EntityFrameworkCore;
using ReportAndSolveAPI.Models;
using ReportAndSolveAPI.Models.DTO.Status;
using ReportAndSolveAPI.Security;
using ReportAndSolveAPI.Services;

namespace ReportAndSolveAPI.Context
{
    public class StatusContext : DbContext, IStatusServices
    {
        private string _connectionString;

        private DbSet<StatusHistoryEntity> _statusHistorys { get; }

        public StatusContext(DbContextOptions<StatusContext> context, IConfiguration configuration) : base(context)
        {
            _connectionString = configuration.GetConnectionString("AdminConnection");

            _statusHistorys = Set<StatusHistoryEntity>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_connectionString);

        public async Task<ServiceResponse<List<GetStatusDTO>>> GetReportStatuses(int reportId)
        {
            var response = new ServiceResponse<List<GetStatusDTO>>();

            try
            {
                var statusesFromDb = _statusHistorys.FromSqlRaw($"select * from getstatushistorytoreport({reportId})")?.ToList();

                if (statusesFromDb is null || statusesFromDb.Count == 0)
                {
                    response = ConfigureResponse<List<GetStatusDTO>>(null, true, "Thers no status history for this post");

                    return response;
                }

                List<GetStatusDTO> status = statusesFromDb.Select(status => new GetStatusDTO() { Statuses = status.statuses }).ToList();

                response = ConfigureResponse<List<GetStatusDTO>>(status, true, "OK");
            } 
            catch (Exception exception) 
            {
                response = ConfigureResponse<List<GetStatusDTO>>(null, false, exception.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateStatus(UpdateStatusDTO newStatus)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();

            try
            {
                await Database.ExecuteSqlRawAsync($"CALL updatestatushistory({newStatus.ReportId}, '{newStatus.NewStatus}')");

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
