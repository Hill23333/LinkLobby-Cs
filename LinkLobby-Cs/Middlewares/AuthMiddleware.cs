using LinkLobby.Models;

namespace LinkLobby.Middlewares
{
    /// <summary>
    /// 自定义验证中间件
    /// </summary>
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // 判断 UserAgent (如果判断 Referer 会引发一堆问题)
            var UserAgentCheck = false;

            // 检查 UserAgent 是否为空
            if (context.Request.Headers.UserAgent.Count > 0)
            {
                foreach (var UserAgent in context.Request.Headers.UserAgent)
                {
                    if (UserAgent!.Contains("PCL2/"))
                    {
                        UserAgentCheck = true;
                    }
                }
            }

            // 判断 
            if (!UserAgentCheck)
            {
                var error = new Error(403);
                context.Response.StatusCode = error.status;
                await context.Response.WriteAsJsonAsync(error);
                return;
            }

            await _next(context);
        }
    }
    public static class MyAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
