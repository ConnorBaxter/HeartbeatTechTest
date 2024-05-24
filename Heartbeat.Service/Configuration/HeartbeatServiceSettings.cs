namespace Heartbeat.Service.Configuration
{
    public class HeartbeatServiceSettings
    {
        public string Url { get; set; }
        public string StatusEndpoint { get; set; }
        public int PollingRate { get; set; }
    }
}