using Infra.Full.Configuration;

namespace WorkerOE
{
  public class Program : IServiceStatus
  {
    public ServiceState CurrentState { get; set; }

    public string ServiceName { get; set; } = "PlatformConsoleService";

    public string ServiceDisplayName { get; set; } = "Platform Console Service";

    public string ServiceDescription { get; set; } = "Platform Console Service";

    public static void Main(string[] args)
    {
      ServiceWrapper.Run(args, new Program(), new Startup());
    }

    public void AfterStart()
    {

    }

    public void AfterStop()
    {

    }

    public void BeforeStart()
    {

    }

    public void BeforeStop()
    {

    }
  }
}
