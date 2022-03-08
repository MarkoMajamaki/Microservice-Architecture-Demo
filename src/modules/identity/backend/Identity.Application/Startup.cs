
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Reflection;

using Shared.Application;

namespace Identity.Application;

public static partial class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IFacebookAuthenticationService, FacebookAuthenticationService>();
        services.AddScoped<IGoogleAuthenticationService, GoogleAuthenticationService>();
        services.AddScoped<IAccessTokenGenerator, AccessTokenGenerator>();
        services.AddMediatR(Assembly.GetExecutingAssembly());

        // Add Facebook auth
        FacebookAuthSettings facebookSettings = configuration.GetSection(FacebookAuthSettings.Key).Get<FacebookAuthSettings>();
        if (facebookSettings != null)
        {
            services.Configure<FacebookAuthSettings>(configuration.GetSection(FacebookAuthSettings.Key));  

            services.AddAuthentication().AddFacebook(facebookOptions => 
            {
                facebookOptions.AppId = facebookSettings.AppId;
                facebookOptions.AppSecret = facebookSettings.AppSecret;
            });
        }

        // Add Google auth
        GoogleAuthSettings googleSettings = configuration.GetSection(GoogleAuthSettings.Key).Get<GoogleAuthSettings>();
        if (googleSettings != null)
        {
            services.Configure<GoogleAuthSettings>(configuration.GetSection(GoogleAuthSettings.Key));  

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = googleSettings.ClientId;
                googleOptions.ClientSecret = googleSettings.ClientSecret;
            });
        }

        // Adding Jwt Bearer settings for authentication
        JwtSettings jwtSettings = configuration.GetSection(JwtSettings.Key).Get<JwtSettings>();
        if (jwtSettings != null)
        {       
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Key));  

            services.AddAuthentication(options =>  
            {  
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;  
            }).AddJwtBearer(options =>  
            {  
                options.SaveToken = true;  
                options.RequireHttpsMetadata = false;  
                options.TokenValidationParameters = new TokenValidationParameters()  
                {  
                    ValidateIssuer = true,  
                    ValidateAudience = true,  
                    ValidAudience = jwtSettings.ValidAudience,  
                    ValidIssuer = jwtSettings.ValidIssuer,  
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))  
                };  
            }); 
        }

        return services;
    }
}