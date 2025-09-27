namespace UrlShortener.Domain.Entities;

public class UrlMapping
{
    public int Id { get; set; }
    public string ShortUrl { get; set; } = default!;
    public string LongUrl { get; set; } = default!;
}