namespace Heartbeat.Api.Tests
{
    using Moq.AutoMock;

    public class TestBase<T> where T : class
    {
        public readonly AutoMocker autoMocker;

        public TestBase()
        {
            this.autoMocker = new AutoMocker();
        }

        public T CreateTestSubject()
        {
            return this.autoMocker.CreateInstance<T>();
        }
    }
}