using Microsoft.Extensions.Configuration;

namespace Portfolio.Server.Extensions
{
  public static class ConfigurationExtensions
  {
    public static string GetDefaultConnectionString(this IConfiguration configuration)
          => configuration.GetConnectionString("DefaultConnection");
  }
}
