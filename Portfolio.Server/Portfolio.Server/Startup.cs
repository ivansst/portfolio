using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio.Server.Services;
using Portfolio.Server.Services.Interfaces;
using Portfolio.Server.Extensions;

namespace Portfolio.Server
{
  public class Startup
  {
    public Startup(IConfiguration configuration) => this.Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDatabase(this.Configuration)
              .AddIdentity()
              .AddJwtAuthentication(services.GetApplicationSettings(this.Configuration))
              .AddSwagger();

      services.AddTransient<IProjectService, ProjectService>()
              .AddTransient<ICommentService, CommentService>();

      services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseSwaggerUI();

      app.UseRouting()
         .UseCors(options => options
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

      app.UseAuthentication()
         .UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.ApplyMigrations();
    }
  }
}
