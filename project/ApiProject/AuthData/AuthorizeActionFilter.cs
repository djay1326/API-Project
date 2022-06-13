using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiProject.AuthData
{
    public class AuthorizeActionFilter : Attribute , IAuthorizationFilter
    {
        private readonly string _permission;


        public AuthorizeActionFilter(string permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = CheckUserPermission(context.HttpContext.User, _permission);

            if (!isAuthorized)
            {
                //return RedirectToActionResult("smallDiisplay");
                context.Result = new UnauthorizedResult();
            }
        }

        private bool CheckUserPermission(ClaimsPrincipal user, string permission)
        {
            // Logic for checking the user permission goes here. 

            // Let's assume this user has only read permission.
            return permission == "Read";
        }
    }
}

