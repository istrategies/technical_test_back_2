using FluentValidation;
using SampleApp.Application.Contracts.DTO;

namespace SampleApp.Application.Contracts.Validators
{
    public class SubSampleValidator : AbstractValidator<SubSample>
    {
        public const string RULE_INSERT = "insert";
        public SubSampleValidator()
        {
            RuleFor(x => x.SubSampleId).NotEmpty();
            RuleFor(x => x.Info).MaximumLength(128);

            RuleSet(RULE_INSERT, () => RuleFor(x => x.SubSampleId));
        }
    }
}