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

            using()

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
}