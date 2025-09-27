using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Application.Services;

public class UrlShortenerService(IUrlMapDb db)
{
    public string ShortenUrl(string longUrl)
    {
        if (string.IsNullOrWhiteSpace(longUrl))
            throw new ArgumentException("URL cannot be null or empty", nameof(longUrl));

        if (!Uri.TryCreate(longUrl, UriKind.Absolute, out _))
            throw new ArgumentException("Invalid URL format", nameof(longUrl));

        var shortUrl = Guid.NewGuid().ToString("N")[..8];

        db.SaveUrlMapping(shortUrl, longUrl);

        return shortUrl;
    }

    public string ExpandUrl(string shortUrl)
    {
        if (string.IsNullOrWhiteSpace(shortUrl))
            throw new ArgumentException("Short URL cannot be null or empty", nameof(shortUrl));

        var longUrl = db.GetLongUrl(shortUrl);
        if (longUrl is null)
            throw new InvalidOperationException("Short URL not found");

        return longUrl;
    }
}