namespace Heartbeat.Service
{
    using Heartbeat.Lib;
    using Heartbeat.Service.Configuration;
    using Heartbeat.Service.MessageSenders;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private readonly HeartbeatServiceSettings settings;
        private readonly IMessageSender messageSender;

        public Worker(ILogger<Worker> logger, 
            HeartbeatServiceSettings settings,
            IMessageSender messageSender)
        {
            this.logger = logger;
            this.settings = settings;
            this.messageSender = messageSender;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                this.logger.LogInformation($"Worker running at: {DateTime.Now.ToUniversalTime():O}");

                var success = await this.messageSender.Send(new StatusResource()
                {
                    Id = Guid.NewGuid(), 
                    TimestampUtc = DateTime.Now.ToUniversalTime()
                });
                
                this.logger.LogInformation($"Message sent successfully: {success}");
                
                await Task.Delay(this.settings.PollingRate * 1000, stoppingToken);
            }
        }
    }
}

