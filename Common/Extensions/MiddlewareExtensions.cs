using Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;

namespace Common.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMemoryCacheInterceptor(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            return app.UseMiddleware<MemoryCacheInterceptor>();
        }

        public static IApplicationBuilder UseRedisCacheInterceptor(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            return app.UseMiddleware<RedisCacheInterceptor>();
        }
    }
}
