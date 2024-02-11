namespace PixelApi.Infrastructure
{
    public class RequestContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public RequestContext(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;

            Referer = this.httpContextAccessor.HttpContext?.Request.Headers["Referer"].ToString() ?? string.Empty;
            UserAgent = this.httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString() ?? string.Empty;
            IpAddress = this.httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
        }

        public string Referer { get; private set; }
        public string UserAgent { get; private set; }
        public string IpAddress { get; private set; }
    }
}
