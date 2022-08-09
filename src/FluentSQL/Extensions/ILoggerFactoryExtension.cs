using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



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
