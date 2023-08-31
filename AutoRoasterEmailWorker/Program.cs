using AutoRoasterEmailWorker.Configurations;
using Microsoft.Extensions.Options;

namespace AutoRoasterEmailWorker;

public class Program
{
    public static void Main(string[] args)
    {
        IHost host = Configure(args);
        host.Run();
    }

    public static IHost Configure(string[] args)
    {
        var builder = new HostApplicationBuilder(args);

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("secrets.json", optional: true, reloadOnChange: true)
            .Build();

        builder.Services
            .Configure<SmtpSettings>(v => builder.Configuration.GetSection("SmtpSettings"));

        _ = builder.Services
            .AddSingleton(configuration)
            .AddSingleton(v => v.GetService<IOptions<SmtpSettings>>().Value)
            .AddSingleton<IEmailSender, EmailSender>()
            .AddHostedService<Worker>();

        return builder.Build();
    }
}
