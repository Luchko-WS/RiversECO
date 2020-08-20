using FluentValidation;
using RiversECO.Dtos.Requests;

namespace RiversECO.Common.Validators.Review
{
    public class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequestDto>
    {
        public UpdateReviewRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Comment)
                .NotEmpty();

            RuleFor(x => x.ModifiedBy)
                .NotEmpty();
        }
    }
}
