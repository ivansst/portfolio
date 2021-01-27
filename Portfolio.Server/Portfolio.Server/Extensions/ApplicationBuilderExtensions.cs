using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Portfolio.Server.Data;

namespace Portfolio.Server.Extensions
{
  public static class ApplicationBuilderExtensions
  {
    public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
           => app
               .UseSwagger()
               .UseSwaggerUI(options =>
               {
                 options.SwaggerEndpoint("/swagger/v1/swagger.json", "My Portfolio API");
                 options.RoutePrefix = string.Empty;
               });

    public static void ApplyMigrations(this IApplicationBuilder app)
    {
      using var services = app.ApplicationServices.CreateScope();

      var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();

      dbContext.Database.Migrate();

      Seeder.Seed(dbContext);
    }
  }
}