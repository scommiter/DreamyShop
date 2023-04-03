using DreamyShop.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using DreamyShop.Domain.Shared.Types;
using DreamyShop.Domain.Shared.Dtos;
using System.Data;

namespace DreamyShop.Api.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var auth = (AuthEntity)context.HttpContext.Items["Auth"];
            if (auth == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }

    public class AdminAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var auth = (AuthEntity)context.HttpContext.Items["Auth"];

            if (!auth.RoleTypes.Any(roleType => roleType == (int)RoleType.Administrator))
            {
                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }

    public class MemberAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var memberRoles = new List<byte?>() { (int)RoleType.Administrator, (int)RoleType.Customer};
            var auth = (AuthEntity)context.HttpContext.Items["Auth"];

            if (!(auth.RoleTypes.Any(roleType => memberRoles.Contains(roleType))))
            {
                context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }
}
