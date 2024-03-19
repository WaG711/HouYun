using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HouYun.Filters
{
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthenticated = context.HttpContext.User.Identity.IsAuthenticated;
            var isLoginPage = IsLoginPage(context.HttpContext.Request.Path);

            if (isAuthenticated && isLoginPage)
            {
                context.Result = new RedirectToActionResult("Index", "Video", null);
            }
        }

        private bool IsLoginPage(string path)
        {
            var normalizedPath = path.ToLowerInvariant();
            return normalizedPath == "/login" ||
                   normalizedPath == "/registration";
        }
    }
}