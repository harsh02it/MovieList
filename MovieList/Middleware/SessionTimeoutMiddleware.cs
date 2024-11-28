using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MovieList.Middleware
{
    public class SessionTimeoutMiddleware : Controller
    {
        private readonly RequestDelegate _next;

        public SessionTimeoutMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the session is active
            if (context.Session.IsAvailable)
            {
                // Check if the user is authenticated
                if (context.User.Identity.IsAuthenticated)
                {
                    // Check if the session has expired
                    if (context.Session.GetString("UserName") == null)
                    {
                        // Log out the user if the session has expired
                        await context.SignOutAsync();
                        context.Response.Redirect("/Account/Login"); // Redirect to login page
                        return;
                    }
                }
            }

            await _next(context);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
