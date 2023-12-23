using System.Configuration;

namespace Core.Application.Pipelines.Caching;

public class CacheSettings
{
    public bool IsActive { get; set; }

    public int SlidingExpiration { get; set; }

    public string Host { get; set; }

    public string Port { get; set; }

    public string Password { get; set; }
}
