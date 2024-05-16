using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ReportAndSolveAPI.Models;
using ReportAndSolveAPI.Models.DTO.Report;
using ReportAndSolveAPI.Services;

namespace ReportAndSolveAPI.Context
{
    public class ReportContext : DbContext, IReportServices
    {
        private string _connectionString;

        private DbSet<ReportEntity> _reports { get; }

        public ReportContext(DbContextOptions<ReportContext> context, IConfiguration configuration) : base(context)
        {
            _connectionString = configuration.GetConnectionString("AdminConnection");

            _reports = Set<ReportEntity>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_connectionString);

        public async Task<ServiceResponse<List<GetReportDTO>>> GetAllReports()
        {
            var response = new ServiceResponse<List<GetReportDTO>>();

            try
            {
                var reportsList = await _reports.ToListAsync();
                var getReportsDTO = reportsList.Select(report => new GetReportDTO() 
                { 
                    Id = report.Id,
                    Author = report.AuthorId,
                    Title = report.Title, 
                    Text = report.Text 
                }).ToList();

                response = ConfigureResponse<List<GetReportDTO>>(getReportsDTO, true, "OK");
            }
            catch (Exception exception)
            {
                response = ConfigureResponse<List<GetReportDTO>>(null, true, "OK");
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetReportDTO>>> GetReportsByAuthorId(int id)
        {
            var repoonse = new ServiceResponse<List<GetReportDTO>>();

            try
            {
                var reportsByAuthor = _reports.FromSqlRaw($"select * from getallreportsbyauthor({id})")?.ToList();

                if (reportsByAuthor is null || reportsByAuthor.Count == 0)
                {
                    repoonse = new ServiceResponse<List<GetReportDTO>>()
                    {
                        Data = null,
                        Succes = true,
                        Message = "Theres no reports from this author",
                    };

                    return repoonse;
                }

                var reportsData = reportsByAuthor.Select(report => new GetReportDTO() 
                { 
                    Id = report.Id,
                    Author = report.AuthorId,
                    Title = report.Title, 
                    Text = report.Text 
                }).ToList();

                repoonse = ConfigureResponse<List<GetReportDTO>>(reportsData, true, "OK");
            }
            catch (Exception exception)
            {
                repoonse = new ServiceResponse<List<GetReportDTO>>()
                {
                    Data = null,
                    Succes = true,
                    Message = exception.Message,
                };
            }

            return repoonse;
        }

        public async Task<ServiceResponse<GetReportDTO>> GetReportById(int id)
        {
            var response = new ServiceResponse<GetReportDTO>();

            try
            {
                var reportById = _reports.FromSqlRaw($"select * from getreportbyid({id})").ToArray();

                if (reportById is null || reportById.Length == 0)
                {
                    response = ConfigureResponse<GetReportDTO>(null, true, "theres no such report with those id");

                    return response;
                }

                var report = new GetReportDTO()
                {
                    Id = reportById[0].Id,
                    Author = reportById[0].AuthorId,
                    Text = reportById[0].Text,
                    Title = reportById[0].Title
                };

                response = ConfigureResponse<GetReportDTO>(report, true, "OK");
            } 
            catch (Exception exception)
            {
                response = ConfigureResponse<GetReportDTO>(null, false, exception.Message);

            }

            return response;
        }

        public async Task<ServiceResponse<bool>> CreateReport(AddReportDTO newReport)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                await Database.ExecuteSqlRawAsync($"CALL createnewreport({newReport.AuthorId}, '{newReport.Title}', '{newReport.Text}')");

                response = ConfigureResponse<bool>(true, true, "OK");
            }
            catch (Exception exception)
            {
                response = ConfigureResponse<bool>(false, false, exception.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateReport(UpdateReportDTO updateReport)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                await Database.ExecuteSqlRawAsync($"CALL updatereport({updateReport.Id}, '{updateReport.Title}', '{updateReport.Text}')");

                response = ConfigureResponse(true, true, "OK");
            } 
            catch (Exception exception)
            {
                response = ConfigureResponse(false, false, exception.Message);
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteReport(int reportId)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                await Database.ExecuteSqlRawAsync($"CALL deletereport({reportId})");

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
