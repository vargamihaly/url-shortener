using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Infrastructure.Persistence;

public class UrlMapContext(DbContextOptions<UrlMapContext> options) : DbContext(options)
{
    public DbSet<UrlMapping> UrlMappings => Set<UrlMapping>();
}