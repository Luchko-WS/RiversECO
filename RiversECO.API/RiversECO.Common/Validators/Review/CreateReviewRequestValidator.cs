using FluentValidation;
using RiversECO.Dtos.Requests;

namespace RiversECO.Common.Validators.Review
{
    public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequestDto>
    {
        public CreateReviewRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Comment)
                .NotEmpty();

            RuleFor(x => x.CreatedBy)
                .NotEmpty();

            RuleFor(x => x.WaterObjectId)
                .NotEmpty();
        }
    }
}
