namespace Heartbeat.Service
{
    using System.Reflection;
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Heartbeat.Service.Configuration;

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
                        .Get<HeartbeatServiceSettings>())
                    .As<HeartbeatServiceSettings>();
            });
            
            builder.Services.AddHostedService<Worker>();
            
            var host = builder.Build();
            host.Run();
        }
    }
}

