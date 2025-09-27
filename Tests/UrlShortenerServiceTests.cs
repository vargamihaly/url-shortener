using Microsoft.Extensions.Caching.Memory;
using Moq;
using UrlShortener.Application.Services;
using UrlShortener.Domain.Interfaces;

namespace Tests;

public class UrlShortenerServiceTests
{
    private readonly UrlShortenerService _service;
    private readonly Mock<IUrlMapDb> _dbMock;
    private readonly IMemoryCache _cache;

    public UrlShortenerServiceTests()
    {
        _dbMock = new Mock<IUrlMapDb>();
        _cache = new MemoryCache(new MemoryCacheOptions());
        _service = new UrlShortenerService(_dbMock.Object, _cache);
    }

    [Fact]
    public void ShortenUrl_ShouldStoreMapping_AndReturnShortCode()
    {
        const string longUrl = "https://github.com/twelve-factor/twelve-factor/blob/main/README.md";

        var code = _service.ShortenUrl(longUrl);

        Assert.False(string.IsNullOrWhiteSpace(code));
        _dbMock.Verify(db => db.SaveUrlMapping(code, longUrl), Times.Once);
    }

    [Fact]
    public void ExpandUrl_ShouldReturnCachedValue()
    {
        const string code = "abc123";
        const string longUrl = "https://github.com/twelve-factor/twelve-factor/blob/main/README.md";

        _cache.Set(code, longUrl);

        var result = _service.ExpandUrl(code);

        Assert.Equal(longUrl, result);
        _dbMock.Verify(db => db.GetLongUrl(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public void ExpandUrl_ShouldThrow_WhenNotFound()
    {
        _dbMock.Setup(db => db.GetLongUrl("missing")).Returns((string?)null);

        Assert.Throws<InvalidOperationException>(() => _service.ExpandUrl("missing"));
    }
}