namespace Heartbeat.Service.MessageSenders
{
    using Heartbeat.Lib;

    public class MessageSender : IMessageSender
    {
        private readonly HttpClient Client = new HttpClient();
        
        public async Task<bool> Send(StatusResource statusResource)
        {
            //this.Client.PostAsJsonAsync()
            return true;
        }
    }
}