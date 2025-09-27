using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Interfaces;

namespace UrlShortener.Infrastructure.Persistence;

public class EfUrlMapDb(UrlMapContext context) : IUrlMapDb
{
    public string? GetLongUrl(string shortUrl) =>
        context.UrlMappings.FirstOrDefault(x => x.ShortUrl == shortUrl)?.LongUrl;

    public void SaveUrlMapping(string shortUrl, string longUrl)
    {
        context.UrlMappings.Add(new UrlMapping { ShortUrl = shortUrl, LongUrl = longUrl });
        context.SaveChanges();
    }
}