using FluentValidation;
using RiversECO.Dtos.Requests;

namespace RiversECO.Common.Validators.Criteria
{
    public class UpdateCriteriaRequestValidator : AbstractValidator<UpdateCriteriaRequestDto>
    {
        public UpdateCriteriaRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
