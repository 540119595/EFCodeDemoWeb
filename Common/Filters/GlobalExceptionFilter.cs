using Common.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            var type = typeof(GlobalExceptionFilter);
            if (exceptionContext.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)
                type = (exceptionContext.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor).ControllerTypeInfo;
            Log4NetHelper.WriteError(type, exceptionContext.Exception);
        }
    }
}
