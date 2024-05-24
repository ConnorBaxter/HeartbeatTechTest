namespace Heartbeat.Service.Tests.MessageSenders
{
    using Heartbeat.Lib;
    using Heartbeat.Service.Configuration;
    using Heartbeat.Service.MessageSenders;
    using Moq;
    using Moq.Protected;

    public class MessageSenderTests : TestBase<MessageSender>
    {
        [Fact]
        public async void Send_ReturnsFalseOnException()
        {
            var stubResource = new StatusResource()
            {
                Id = Guid.NewGuid(),
                TimestampUtc = new DateTime(2024, 05, 24)
            };
            
            var stubSettings = new HeartbeatServiceSettings()
            {
                Url = "http://unit.test/",
                StatusEndpoint = "test"
            };
            
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new Exception())
                .Verifiable();

            var stubClient = new Mock<HttpClient>();
            
            this.autoMocker.Use(stubClient.Object);
            this.autoMocker.Use(stubSettings);
            
            var sut = this.CreateTestSubject();
            var result = await sut.Send(stubResource);
            
            Assert.False(result);
        }
    }
}