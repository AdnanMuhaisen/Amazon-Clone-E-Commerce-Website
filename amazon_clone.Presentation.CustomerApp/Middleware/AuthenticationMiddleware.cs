using amazon_clone.Domain.Users.CurrentUsers;

namespace amazon_clone.Presentation.CustomerApp.Middleware
{
    public class AuthenticationMiddleware
    {
        //public RequestDelegate _next { get; set; }
        //public AuthenticationMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //}

        //public async Task Invoke(HttpContext httpContext)
        //{
        //    if (httpContext.Request.Path.StartsWithSegments("Identity/Account"))
        //        await _next(httpContext);

        //    if (CurrentCustomer.UserID is null)
        //        httpContext.Response.Redirect("/LoginAndRegister/Index");

        //    await _next(httpContext);
        //}

        //public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        //{
        //    if (context.Request.Path.StartsWithSegments("Identity/Account"))
        //         await next(context);

        //    if (CurrentCustomer.UserID is null)
        //        context.Response.Redirect("/LoginAndRegister/Index");

        //    await next(context);
        //}
    }
}
