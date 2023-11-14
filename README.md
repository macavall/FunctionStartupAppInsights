# FunctionStartupAppInsights
Using App Insights in Program.cs for .net 6.0 isolated Function App using Function Host V4

``` JSON
{
  "version": "2.0",
  "logging": {
    "fileLoggingMode": "always",
    "logLevel": {
      "default": "Trace",
      "Microsoft": "Trace",
      "Worker": "Trace",
      "Microsoft.ApplicationInsights": "Trace"
    },
    "console": {
      "isEnabled": true,
      "DisableColors": true
    },
    "applicationInsights": {
      "samplingSettings": {
        "isEnabled": true,
        "excludedTypes": "Request"
      },
      "enableLiveMetricsFilters": true
    }
  }
}
```
---

It is possible to use the `Console.WriteLine` method for writing out to the Kudu `C:\home\LogFiles\Application\Functions\Host` directory

An Example of what is logged out from this startup:

``` csharp
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;

internal class Program
{
    private static void Main(string[] args)
    {

        try
        {
            var config = new TelemetryConfiguration()
            {
                ConnectionString = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING")
            };

            var telemetryClient = new TelemetryClient(config);

            telemetryClient.TrackTrace("STARTING FUNCTION APP!");

            Console.WriteLine("STARTING FUNCTION APP CONSOLE!");

            var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices(services => {
                services.AddApplicationInsightsTelemetryWorkerService();
                services.ConfigureFunctionsApplicationInsights();
            })
            .Build();

            if (Environment.GetEnvironmentVariable("fail").ToLower() == "true")
            {
                throw new Exception();
            }

            Console.WriteLine("RUNNING host.Run()!");
            host.Run();
        }
        catch (Exception ex)
        {
            var config = new TelemetryConfiguration()
            {
                ConnectionString = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING")
            };

            var telemetryClient = new TelemetryClient(config);

            telemetryClient.TrackTrace("EXCEPTION FOUND!!!");

            Console.WriteLine("EXCEPTION FOUND!!! CONSOLE");
            Console.WriteLine(ex.Message + "CONSOLE");
            Console.WriteLine(ex.StackTrace + "CONSOLE");
        }
    }

    private void OnShutdown()
    {
        Console.WriteLine("SHUTTING DOWN!!!");

        TelemetryClient client = new TelemetryClient();
        client.Flush();

        // Allow time for flushing:
        System.Threading.Thread.Sleep(5000);
        Console.WriteLine("SHUTTING COMPLETE!!!");
    }

}
```

In the Kudu Functions App Logs shows the following:

![image](https://github.com/macavall/FunctionStartupAppInsights/assets/43223084/355ee217-57db-44e8-9d05-0eeb034f7955)

Application Insights will show the following details:

![image](https://github.com/macavall/FunctionStartupAppInsights/assets/43223084/38d996c6-f4c8-4ad7-a68a-c21958645b6c)
