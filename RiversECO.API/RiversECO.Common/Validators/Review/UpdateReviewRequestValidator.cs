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

            RuleFor(x => x.ModifiedBy)
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
