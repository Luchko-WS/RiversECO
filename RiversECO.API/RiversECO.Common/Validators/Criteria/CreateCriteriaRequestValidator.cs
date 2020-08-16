using FluentValidation;
using RiversECO.Dtos.Requests;

namespace RiversECO.Common.Validators.Criteria
{
    public class CreateCriteriaRequestValidator : AbstractValidator<CreateCriteriaRequestDto>
    {
        public CreateCriteriaRequestValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
