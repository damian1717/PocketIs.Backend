using Microsoft.AspNetCore.Builder;

namespace PocketIS.Application.Common.Mvc
{
    public static class Extensions
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
            => builder.UseMiddleware<ErrorHandlerMiddleware>();
    }
}
