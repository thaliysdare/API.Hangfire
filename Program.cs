using API.Hangfire;
using Hangfire;
using Hangfire.Storage.SQLite;

var builder = WebApplication.CreateBuilder(args);

GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 3, DelaysInSeconds = [300] });

builder.Services
.AddEndpointsApiExplorer()
.AddSwaggerGen()
.AddHangfire(config => config
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage())
.AddHangfireServer();

var app = builder.Build();

app
.UseSwagger()
.UseSwaggerUI()
.UseHttpsRedirection()
.UseHangfireDashboard();

HangfireConfig.ConfigureRecurringJob();

app.MapGet("/job", () =>
{
    BackgroundJob.Enqueue(() => HangfireConfig.JobExecutado());
    return Results.Ok("Job disparado");
});

app.MapGet("/job/com-erro", () =>
{
    BackgroundJob.Enqueue(() => HangfireConfig.JobComErro());
    return Results.Ok("Job disparado");
});

app.MapGet("/job/com-parametro", (string parametro) =>
{
    BackgroundJob.Enqueue(() => HangfireConfig.JobComParametro(parametro));
    return Results.Ok($"Job disparado -> {parametro}");
});

app.Run();