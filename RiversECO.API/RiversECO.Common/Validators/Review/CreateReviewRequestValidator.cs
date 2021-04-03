using FluentValidation;
using RiversECO.Dtos.Requests;

namespace RiversECO.Common.Validators.Review
{
    public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequestDto>
    {
        public CreateReviewRequestValidator()
        {
            RuleFor(x => x.CreatedBy)
                .NotEmpty();

            RuleFor(x => x.WaterObjectId)
                .NotEmpty();

            RuleFor(x => x.CriteriaName)
                .NotEmpty();

            RuleFor(x => x.Influence)
                .NotEmpty();

            RuleFor(x => x.ReferenceType)
                .NotEmpty();

            RuleFor(x => x.Reference)
                .NotEmpty();

            RuleFor(x => x.Comment)
                .NotEmpty();
        }
    }
}
