using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Sellorio.Results.Messages;

namespace Sellorio.Validation.Helpers;

internal static class ResultMessageHelper
{
    public static ResultMessage CreateMessage(string message, ResultMessageSeverity severity)
    {
        return severity switch
        {
            ResultMessageSeverity.Critical => ResultMessage.Critical(message),
            ResultMessageSeverity.Error => ResultMessage.Error(message),
            ResultMessageSeverity.NotFound => ResultMessage.NotFound(message),
            ResultMessageSeverity.Warning => ResultMessage.Warning(message),
            ResultMessageSeverity.Information => ResultMessage.Information(message),
            _ => throw new NotSupportedException()
        };
    }

    public static ResultMessage CreateMessage<TContext>(string message, ResultMessageSeverity severity, Expression<Func<TContext, object>> path)
    {
        return severity switch
        {
            ResultMessageSeverity.Critical => ResultMessage.Critical(path, message),
            ResultMessageSeverity.Error => ResultMessage.Error(path, message),
            ResultMessageSeverity.NotFound => ResultMessage.NotFound(path, message),
            ResultMessageSeverity.Warning => ResultMessage.Warning(path, message),
            ResultMessageSeverity.Information => ResultMessage.Information(path, message),
            _ => throw new NotSupportedException()
        };
    }

    public static ResultMessage CreateMessage(string message, ResultMessageSeverity severity, IList<ResultMessagePathItem>? path)
    {
        if (path == null)
        {
            return CreateMessage(message, severity);
        }

        return severity switch
        {
            ResultMessageSeverity.Critical => ResultMessage.Critical(path, message),
            ResultMessageSeverity.Error => ResultMessage.Error(path, message),
            ResultMessageSeverity.NotFound => ResultMessage.NotFound(path, message),
            ResultMessageSeverity.Warning => ResultMessage.Warning(path, message),
            ResultMessageSeverity.Information => ResultMessage.Information(path, message),
            _ => throw new NotSupportedException()
        };
    }
}
