using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReportAndSolveAPI.Context;
using ReportAndSolveAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ReportContext>();
builder.Services.AddDbContext<ReportContext>();
builder.Services.AddScoped<IReportServices, ReportContext>();

builder.Services.AddScoped<UserContext>();
builder.Services.AddDbContext<UserContext>();
builder.Services.AddScoped<IUserServices, UserContext>();

builder.Services.AddScoped<CommentContext>();
builder.Services.AddDbContext<CommentContext>();
builder.Services.AddScoped<ICommentServices, CommentContext>();

builder.Services.AddScoped<StatusContext>();
builder.Services.AddDbContext<StatusContext>();
builder.Services.AddScoped<IStatusServices, StatusContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();