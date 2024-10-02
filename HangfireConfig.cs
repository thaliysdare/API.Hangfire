using Hangfire;

namespace API.Hangfire;

public class HangfireConfig
{
    public static void ConfigureRecurringJob() =>
        RecurringJob.AddOrUpdate("jobId", () => JobExecutado(), "*/5 * * * *");

    public static void JobComAgendamento() =>
        Console.WriteLine("Job com agendamento executado");
    
    public static void JobExecutado() =>
        Console.WriteLine("Job executado");

    public static void JobComErro() =>
        throw new Exception("Erro ao executar o job");
    
    public static void JobComParametro(string parametro) =>
        Console.WriteLine($"Job executado com o parametro {parametro}");
}