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
        //private TelemetryClient telemetryClient;

        try
        {
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

            host.Run();
        }
        catch (Exception ex)
        {
            var config = new TelemetryConfiguration()
            {
                ConnectionString = Environment.GetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING")
            };

            var telemetryClient = new TelemetryClient(config);

            telemetryClient.TrackTrace("FAILED5689");

            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }

    }
}