using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Portfolio.Server.Data;
using Portfolio.Server.Data.Models;
using System.Text;

namespace Portfolio.Server.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static AppSettings GetApplicationSettings(this IServiceCollection services, IConfiguration configuration)
    {
      var applicationSettingsConfiguration = configuration.GetSection("ApplicationSettings");
      services.Configure<AppSettings>(applicationSettingsConfiguration);

      return applicationSettingsConfiguration.Get<AppSettings>();
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<ApplicationDbContext>(options => options
                       .UseSqlServer(configuration.GetDefaultConnectionString()));

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
      services
          .AddIdentity<User, IdentityRole>(options =>
          {
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
          })
          .AddEntityFrameworkStores<ApplicationDbContext>();

      return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AppSettings appSettings)
    {
      var key = Encoding.ASCII.GetBytes(appSettings.Secret);

      services
          .AddAuthentication(x =>
          {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
          .AddJwtBearer(x =>
          {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(key),
              ValidateIssuer = false,
              ValidateAudience = false
            };
          });

      return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
      var securityScheme = new OpenApiSecurityScheme
      {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
      };

      var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                };

      return services.AddSwaggerGen(c =>
                    {
                      c.SwaggerDoc("v1", new OpenApiInfo
                      {
                        Title = "PortfolioAPI",
                        Version = "v1"
                      });

                      c.AddSecurityDefinition("Bearer", securityScheme);

                      c.AddSecurityRequirement(securityRequirement);
                    });
    }
  }
}
