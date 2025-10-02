using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Task_12.Middlewares
{
    public class SendToError500PageMiddleware
    {
        private readonly RequestDelegate _next;

        public SendToError500PageMiddleware(RequestDelegate next)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next), $"{nameof(next)} is null.");
            }

            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), $"{nameof(context)} is null.");
            }

            int statusCode = context.Response.StatusCode;
            string path = context.Request.Path;

            if (context.Response.StatusCode == StatusCodes.Status500InternalServerError)
            {
                context.Response.Redirect("/Error500");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
