using Serilog;

namespace ImaginaryRealEstate;

public static class LoggerConfig
{
    public static ILoggingBuilder AddMyLogger(this ILoggingBuilder logging)
    {
        var logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("./logger.log", fileSizeLimitBytes: 12000, rollOnFileSizeLimit: true, shared:true)
            .CreateLogger();
        
        logging.AddSerilog(logger);

        return logging;
    }
}