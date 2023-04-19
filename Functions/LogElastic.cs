using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using Serilog.Sinks.Elasticsearch;
using Serilog.Events;

namespace WorkerOE.Functions
{
    public class LogElastic
    {
        public void configElastic()
        {
            Log.Logger = new LoggerConfiguration()
          .Enrich.FromLogContext()
          .WriteTo.Console()
          .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
          {
              AutoRegisterTemplate = true,
              IndexFormat = "logeo-{0:yyyy.MM.dd}",
              MinimumLogEventLevel = LogEventLevel.Information
          })
          .CreateLogger();
        }
    }

}
