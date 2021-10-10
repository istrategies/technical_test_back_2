using FluentValidation;
using SampleApp.Application.Contracts.DTO;

namespace SampleApp.Application.Contracts.Validators
{
    public class SampleFilterValidator : AbstractValidator<SampleFilter>
    {
        public SampleFilterValidator()
        {
            RuleFor(x => x.StartCreated).NotEmpty();
            RuleFor(x => x.EndCreated).NotEmpty();
            RuleFor(x => x.EndCreated).GreaterThan(y => y.StartCreated);
            RuleFor(x => x.Rows).GreaterThan(0);
            RuleFor(x => x.CurrentPage).GreaterThan(0);
        }
    }
}
