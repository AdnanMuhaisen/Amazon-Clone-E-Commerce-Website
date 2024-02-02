using amazon_clone.Domain.Users.CurrentUsers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace amazon_clone.Presentation.CustomerApp.Filters
{
    public class AuthenticationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //if (CurrentCustomer.UserID is null)
            //    filterContext.HttpContext.Response.Redirect("/LoginAndRegister/Index");
        }
    }
}
