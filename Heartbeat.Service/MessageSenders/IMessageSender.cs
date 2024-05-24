namespace Heartbeat.Service.MessageSenders
{
    using Heartbeat.Lib;

    public interface IMessageSender
    {
        Task<bool> Send(StatusResource statusResource);
    }
}