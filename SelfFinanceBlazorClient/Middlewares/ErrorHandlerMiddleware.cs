using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace SelfFinanceBlazorClient.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
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

            await _next(context);
        }
    }
}
