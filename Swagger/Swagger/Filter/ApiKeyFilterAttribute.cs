using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Swagger.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyFilterAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _apiKey;

        public ApiKeyFilterAttribute()
        {
            this._apiKey = "qwer1234asdf5678zxcv";
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var auth = context.HttpContext.Request.Headers.Authorization.FirstOrDefault();
            if (string.IsNullOrEmpty(auth))
            {
                context.Result = new UnauthorizedObjectResult("Missing Authorization.");
                return;
            }

            if (string.Compare(auth, this._apiKey, StringComparison.OrdinalIgnoreCase) != 0)
            {
                context.Result = new UnauthorizedObjectResult("Invalid api key.");
                return;
            }
        }
    }
}
