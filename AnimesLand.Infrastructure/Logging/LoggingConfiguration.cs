using Serilog;

namespace AnimesLand.Infrastructure.Logging
{
    public static class LoggingConfiguration
    {
        public static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("Logs/app_log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }
    }
}
