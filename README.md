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
