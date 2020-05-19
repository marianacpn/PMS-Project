using Application.Users.Queries.GetAuthenticatedUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using WebUI.Attributes;
using WebUI.Common;

namespace WebUI.Filters
{
    public class SessionExpireFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var validateController = (filterContext.ActionDescriptor as ControllerActionDescriptor).ControllerTypeInfo.GetCustomAttributes(typeof(SkipSessionValidationAttribute), true).Any();
            if (validateController)
                return;

            var validateAction = (filterContext.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(typeof(SkipSessionValidationAttribute), true).Any();
            if (validateAction)
                return;

            HttpContext ctx = filterContext.HttpContext;

            if (ctx.Session.GetObjectFromJson<UserAuthenticatedVm>("User") == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Index").WithDangerMessage("Sua sessão expirou, faça o login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
