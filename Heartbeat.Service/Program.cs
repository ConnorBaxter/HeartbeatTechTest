namespace Heartbeat.Service
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Heartbeat.Service.Configuration;

    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            
            builder.Services.AddOptions();
            builder.ConfigureContainer(new AutofacServiceProviderFactory(), containerBuilder =>
            {
                containerBuilder
                    .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                    .AsImplementedInterfaces();
                
                containerBuilder
                    .Register<HeartbeatServiceSettings>(_ => builder.Configuration
                        .GetSection(nameof(HeartbeatServiceSettings))
                        .Get<HeartbeatServiceSettings>() ?? throw new InvalidOperationException())
                    .As<HeartbeatServiceSettings>();
            });
            
            var host = builder.Build();
            host.Run();
        }
    }
}

