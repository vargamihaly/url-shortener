using Microsoft.Extensions.Caching.Memory;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Application.Services;

public class UrlShortenerService(IUrlMapDb db, IMemoryCache cache)
{
    private static readonly MemoryCacheEntryOptions CacheOptions = new()
    {
        SlidingExpiration = TimeSpan.FromMinutes(10),
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
    };

    public string ShortenUrl(string longUrl)
    {
        if (string.IsNullOrWhiteSpace(longUrl))
            throw new ArgumentException("URL cannot be null or empty", nameof(longUrl));

        if (!Uri.TryCreate(longUrl, UriKind.Absolute, out _))
            throw new ArgumentException("Invalid URL format", nameof(longUrl));

        var shortUrl = Guid.NewGuid().ToString("N")[..8];

        db.SaveUrlMapping(shortUrl, longUrl);
        cache.Set(shortUrl, longUrl, CacheOptions);

        return shortUrl;
    }

    public string ExpandUrl(string shortUrl)
    {
        if (string.IsNullOrWhiteSpace(shortUrl))
            throw new ArgumentException("Short URL cannot be null or empty", nameof(shortUrl));

        if (cache.TryGetValue(shortUrl, out string? cachedLongUrl))
        {
            return cachedLongUrl!;
        }

        var longUrl = db.GetLongUrl(shortUrl);
        if (longUrl is null)
            throw new InvalidOperationException("Short URL not found");

        cache.Set(shortUrl, longUrl, CacheOptions);
        return longUrl;
    }
}