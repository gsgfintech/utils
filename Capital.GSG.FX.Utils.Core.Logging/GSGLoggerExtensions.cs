﻿using Microsoft.Extensions.Logging;
using System;

namespace Capital.GSG.FX.Utils.Core.Logging
{
    public static class GSGLoggerExtensions
    {
        public static void Debug(this ILogger logger, string message)
        {
            logger.LogDebug(message);
        }

        public static void Info(this ILogger logger, string message)
        {
            logger.LogInformation(message);
        }

        public static void Error(this ILogger logger, string message)
        {
            logger.LogError(message);
        }

        public static void Error(this ILogger logger, string message, Exception ex)
        {
            logger.LogError(new EventId(), ex, message);
        }

        public static void Fatal(this ILogger logger, string message)
        {
            logger.LogCritical(message);
        }

        public static void Fatal(this ILogger logger, string message, Exception ex)
        {
            logger.LogCritical(new EventId(), ex, message);
        }
    }
}
