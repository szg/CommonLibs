using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace CommonLibs.Middlewares
{
    /// <summary>
    /// 自定义健康监测中间件
    /// </summary>
    public class HealthCheckMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="next"></param>
        public HealthCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 委托方法
        /// </summary>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Content-Type", "application/json;charset=utf-8");
            await context.Response.WriteAsync(
                      new
                      {
                          Status = "ok",
                          Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                      }.ToJson());
        }
    }

    /// <summary>
    /// 自定义健康监测扩展
    /// </summary>
    public static class HealthCheckMiddlewareExtensions
    {
        /// <summary>
        /// 自定义健康监测
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder builder)
        {
            //健康检测
            builder.MapWhen(context => context.Request.Path.Equals("/health"), application =>
            {
                application.UseMiddleware<HealthCheckMiddleware>();
            });
            return builder;
        }
    }
}
