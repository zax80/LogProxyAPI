namespace LogProxyWebAPI.Authentication
{
    using LogProxyWebAPI.Services;
    using System.Net.Http.Headers;
    using System.Text;

    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogProxyService logProxyService)
        {
            try
            {
                AuthenticationHeaderValue authHeader = AuthenticationHeaderValue.Parse(context.Request.Headers["Authorization"]);
                byte[] credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                string[] credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                string username = credentials[0];
                string password = credentials[1];

                // authenticate credentials with user service and attach user to http context
                context.Items["User"] = await logProxyService.Authenticate(username, password);
            }
            catch
            {
                // do nothing if invalid auth header
                // user is not attached to context so request won't have access to secure routes
            }

            await _next(context);
        }
    }
}