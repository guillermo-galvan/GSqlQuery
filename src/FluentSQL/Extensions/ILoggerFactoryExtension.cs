using Microsoft.Extensions.Logging;

namespace FluentSQL.Extensions
{
    internal static class ILoggerFactoryExtension
    {
        internal static void LogWarning<T>(this ILoggerFactory factory, string? message, params object?[] args)
        {
            ILogger logger =  factory.CreateLogger<T>();
            logger.LogWarning(message, args);
        }
    }
}
