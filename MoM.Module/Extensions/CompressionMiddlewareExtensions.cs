using Microsoft.AspNetCore.Builder;
using MoM.Module.Middleware;

namespace MoM.Module.Extensions
{
    public static class CompressionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCompression(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CompressionMiddleware>();
        }
    }
}
