using Infra.Full.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WorkerOE
{
  public class Startup : IStartup
  {
    public void Configure(HostBuilderContext context, IServiceCollection services)
    {
            // Add your services here....
            services.AddDataAccessRegistry();
            services.AddHostedService<WorkerFilterCheck>();
        }
    public void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder config)
    {
    }
  }
}
