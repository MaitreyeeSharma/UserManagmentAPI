namespace UserManagementAPI.Middleware
{
    public class TokenAuthMiddleware
    {
        private readonly RequestDelegate _next;
        public TokenAuthMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer ", "");
            if (token != "valid-token")
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            await _next(context);
        }
    }

}
