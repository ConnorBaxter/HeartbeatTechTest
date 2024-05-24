namespace Heartbeat.Api.Tests.Controllers
{
    using Heartbeat.Api.Controllers;
    using Heartbeat.Lib;
    using Microsoft.AspNetCore.Mvc;

    public class StatusControllerTests : TestBase<StatusController>
    {
        [Fact]
        public async void PostHeartbeat_ReturnsOkay()
        {
            var stubResource = new StatusResource()
            {
                Id = Guid.NewGuid(),
                TimestampUtc = new DateTime(2024, 05, 24)
            };
            
            var sut = this.CreateTestSubject();
            var result = await sut.PostHeartbeat(stubResource);

            Assert.IsType<OkResult>(result);
        }
    }
}