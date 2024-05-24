namespace Heartbeat.Service.MessageSenders
{
    using System.Net.Http.Json;
    using Heartbeat.Lib;
    using Heartbeat.Service.Configuration;

    public class MessageSender : IMessageSender
    {
        private readonly HttpClient Client = new();
        
        private readonly HeartbeatServiceSettings settings;

        public MessageSender(HeartbeatServiceSettings settings)
        {
            this.settings = settings;
        }

        public async Task<bool> Send(StatusResource statusResource)
        {
            var endpoint = this.settings.Url + this.settings.StatusEndpoint;
            var response = await this.Client.PostAsJsonAsync(endpoint, statusResource);
            return response.IsSuccessStatusCode;
        }
    }
}