using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using RiversECO.Common.Validators.Criteria;
using RiversECO.Common.Validators.Review;
using RiversECO.Dtos.Requests;
using RiversECO.Models;

namespace RiversECO.API.Extensions
{
    public static class ConfigurationExt
    {
        public static IServiceCollection ConfigureIdentitySystem(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext.DataContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();

            return services;
        }

        public static IServiceCollection ConfigureAuth(this IServiceCollection services, string token)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options => {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token)),
                      ValidateIssuer = false,
                      ValidateAudience = false
                  };
              });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            });

            return services;
        }

        public static IServiceCollection ConfigureStaticFiles(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "wwwroot";
            });

            return services;
        }

        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            return services
                .AddTransient<IValidator<CreateCriteriaRequestDto>, CreateCriteriaRequestValidator>()
                .AddTransient<IValidator<UpdateCriteriaRequestDto>, UpdateCriteriaRequestValidator>()
                .AddTransient<IValidator<CreateReviewRequestDto>, CreateReviewRequestValidator>()
                .AddTransient<IValidator<UpdateReviewRequestDto>, UpdateReviewRequestValidator>();
        }
    }
}
