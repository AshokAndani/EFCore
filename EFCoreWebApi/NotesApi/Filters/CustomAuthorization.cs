using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using NotesApi.Controllers;

namespace NotesApi.Filters;

public class ProtectedAttribute : ActionFilterAttribute
{
    public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var tokenDetails = context.HttpContext.Request.Headers.Where(x => x.Key.Contains("x-session-id")).FirstOrDefault();

        var controller = context.Controller as BaseController;
        var value = tokenDetails.Value.FirstOrDefault();
        if (value != null)
        {
            var tokenModel = await controller?.context?.Tokens?.Where(x => x.Token.Equals(value))?.FirstOrDefaultAsync();
            if(tokenModel != null)
            {
                if (tokenModel.CreatedDate < DateTime.UtcNow.AddMinutes(-30))
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                controller.User = await controller?.context?.Users?.Where(x=>x.Id == tokenModel.UserId)?.FirstOrDefaultAsync();
               await base.OnActionExecutionAsync(context, next);
            }
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
    }
}
