namespace Heartbeat.Service.MessageSenders
{
    using System.Net.Http.Json;
    using Heartbeat.Lib;
    using Heartbeat.Service.Configuration;

    public class MessageSender : IMessageSender
    {
        private readonly HttpClient client = new();
        
        private readonly ILogger<MessageSender> logger;
        private readonly HeartbeatServiceSettings settings;

        public MessageSender(HeartbeatServiceSettings settings, 
            ILogger<MessageSender> logger)
        {
            this.settings = settings;
            this.logger = logger;
        }

        public async Task<bool> Send(StatusResource statusResource)
        {
            var endpoint = this.settings.Url + this.settings.StatusEndpoint;

            try
            {
                var response = await this.client.PostAsJsonAsync(endpoint, statusResource);
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                this.logger.LogError(e, "Api is unavailable");
                return false;
            }
        }
    }
}