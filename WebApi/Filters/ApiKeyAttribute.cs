using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters {

    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]

    public class ApiKeyAttribute : Attribute, IAsyncActionFilter {


        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {

            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("ApiKey");

            // Här tar den key från URL
            if (!context.HttpContext.Request.Query.TryGetValue("key", out var providedKey)) {

                context.Result = new UnauthorizedResult();
                return;
            }

            //// Här tar den från Headern
            //if (!context.HttpContext.Request.Headers.TryGetValue("key", out var providedKey)) {

            //    context.Result = new UnauthorizedResult();
            //    return;
            //}

            if (!apiKey.Equals(providedKey)) {

                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
