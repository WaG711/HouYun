using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class CustomAuthorizationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity.IsAuthenticated
            && (context.HttpContext.Request.Path == "/login/index"
            || context.HttpContext.Request.Path == "/registration/index"))
        {
            context.Result = new RedirectToActionResult("index", "video", null);
        }
    }
}
