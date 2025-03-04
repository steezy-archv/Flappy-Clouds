using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Flappy_Clouds.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class UserRoleMiddleware
{
    private readonly RequestDelegate _next;

    public UserRoleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, FlappyCloudsContext db)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var email = context.User.Identity.Name;
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                // Store role in HttpContext.Items
                context.Items["UserRole"] = user.Role;
            }
        }

        await _next(context);
    }
}
