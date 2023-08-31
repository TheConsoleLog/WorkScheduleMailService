namespace AutoRoasterEmailWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IEmailSender _emailSender;
    private DateTime _lastSent;

    public Worker(IEmailSender emailSender, ILogger<Worker> logger)
    {
        _logger = logger;
        _emailSender = emailSender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromMinutes(2));
        while (await timer.WaitForNextTickAsync() && !stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            if (!IsTimeToSend())
            {
                _logger.LogTrace("Not time to send... Continue");
                continue;
            }
            await _emailSender.SendEmailAsync("test", "test", "test");
        }
    }


    protected bool IsTimeToSend()
    {
        DateTime dt = DateTime.Today;

        if(dt.DayOfWeek.Equals(DayOfWeek.Friday) &&
            dt.Hour.Equals(12) &&
            dt.Subtract(_lastSent).TotalDays >= 6)
        {
            _lastSent = dt;
            return true;
        }

        return false;
    }
}

