using System.Text;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Northwind.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger<LogActionFilter> logger;
        private readonly bool logParams;

        public LogActionFilter(ILoggerFactory loggerFactory, bool logParams = false)
        {
            this.logParams = logParams;
            logger = loggerFactory.CreateLogger<LogActionFilter>();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var logEntry =
                new StringBuilder(
                    $"Executing {((ControllerActionDescriptor) context.ActionDescriptor).ActionName} for {((ControllerActionDescriptor) context.ActionDescriptor).ControllerName}");
            if (logParams)
            {
                foreach (var contextActionArgument in context.ActionArguments)
                {
                    logEntry.AppendLine();
                    logEntry.Append($"{contextActionArgument.Key}: {contextActionArgument.Value}");
                }
            }

            logger.LogInformation(logEntry.ToString());
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation(
                $"Executed {((ControllerActionDescriptor) context.ActionDescriptor).ActionName} for {((ControllerActionDescriptor) context.ActionDescriptor).ControllerName}");
        }
    }
}