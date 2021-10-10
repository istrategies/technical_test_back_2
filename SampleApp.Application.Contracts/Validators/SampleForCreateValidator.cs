using FluentValidation;
using SampleApp.Application.Contracts.DTO;

namespace SampleApp.Application.Contracts.Validators
{
    public class SampleForCreateValidator : AbstractValidator<SampleForCreate>
    {
        public SampleForCreateValidator(IValidator<SubSample> subsampleValidator)
        {
            RuleFor(x => x.SampleId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(32);
            RuleForEach(x => x.SubSamples).SetValidator(subsampleValidator, SubSampleValidator.RULE_INSERT);

        }
    }
}
