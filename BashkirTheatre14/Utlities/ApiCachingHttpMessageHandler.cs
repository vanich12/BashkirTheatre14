using System.Net.Http;
using Microsoft.Extensions.Caching.Memory;

namespace BashkirTheatre14.Utlities
{
    public class ApiCachingHttpMessageHandler(IMemoryCache cache) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var query = request.RequestUri;
            if (query is not null && cache.TryGetValue(query, out var res) && res is string s)
            {
                return new HttpResponseMessage
                {
                    Content = new StringContent(s)
                };
            }
            var unCachedResult = await base.SendAsync(request, cancellationToken);
            var uncachedContent = await unCachedResult.Content.ReadAsStringAsync(cancellationToken);
            if(query is not null)
                cache.Set(query, uncachedContent, TimeSpan.FromMinutes(10));
            return unCachedResult;
        }
    }
}
