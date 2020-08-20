using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using RiversECO.Common.Validators.Criteria;
using RiversECO.Common.Validators.Review;
using RiversECO.Dtos.Requests;

namespace RiversECO.API.Extensions
{
    public static class ConfigurationExt
    {
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
