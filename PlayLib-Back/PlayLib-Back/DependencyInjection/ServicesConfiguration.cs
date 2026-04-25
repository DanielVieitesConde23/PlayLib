using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlayLib.Application.Interfaces;
using PlayLib.Application.Interfaces.Repositories;
using PlayLib.Application.Services;
using PlayLib.Application.Services.Repositories;
using PlayLib.Data.Entities;
using PlayLib.Data.Responses;
using System.Text;

namespace PlayLib_Back.DependencyInjection;

public static class ServicesConfiguration {
    public static IServiceCollection Services(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PlayLibDContext>(options =>
        {
            options.UseSqlServer(configuration.GetSection("ConnectionStrings:DeveloperConnection").Value);
        });

        services.AddCors(option =>
        {
            option.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });

        services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        );

        AuthConfiguration authConfiguration = new AuthConfiguration();

        configuration.Bind("Authentication", authConfiguration);

        services.AddSingleton(authConfiguration);
        services.AddScoped<IPasswordHasher, BcriptPasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthService, AuthService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.Key)),
                ValidIssuer = authConfiguration.Issuer,
                ValidAudience = authConfiguration.Audience,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }
}
