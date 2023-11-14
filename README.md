# FunctionStartupAppInsights
Using App Insights in Program.cs for .net 6.0 isolated Function App using Function Host V4

``` JSON
{
  "version": "2.0",
  "tracing": {
    "consoleLevel": "verbose"
  },
  "logging": {
    "fileLoggingMode": "always",
    "logLevel": {
      "default": "Trace",
      "Microsoft": "Trace",
      "Worker": "Trace",
      "Microsoft.ApplicationInsights": "Trace"
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

It is possible to use the `Console.WriteLine` method for writing out to the Kudu C:\home\LogFiles\Application\Function
