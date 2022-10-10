using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JWT.AttributeFilter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAllowAnonymous = context.ActionDescriptor
                                   .EndpointMetadata
                                   .OfType<AllowAnonymousAttribute>().Any();

            if (isAllowAnonymous)
                return;

            long nUser = 0;

            foreach (var item in context.HttpContext.User.Claims)
            {
                if (item.Type == "idUser")
                {
                    if (long.TryParse(item.Value, out long accountId))
                        nUser = accountId;

                    break;
                } 
            }

            if (nUser <= 0)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) 
                { 
                    StatusCode = StatusCodes.Status401Unauthorized 
                };
            }
        }
    }
}
