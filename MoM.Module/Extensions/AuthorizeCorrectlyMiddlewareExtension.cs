using Microsoft.AspNetCore.Builder;
using MoM.Module.Middleware;

namespace MoM.Module.Extensions
{
    public static class AuthorizeCorrectlyMiddlewareExtension
    {
        public static IApplicationBuilder UseAuthorizeCorrectly(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizeCorrectlyMiddleware>();
        }
    }
}
