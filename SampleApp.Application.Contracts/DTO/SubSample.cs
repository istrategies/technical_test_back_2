using FluentValidation;
using SampleApp.Application.Contracts.Validators;
using System;

namespace SampleApp.Application.Contracts.DTO
{
    /// <summary>
    /// DTO with the SubSample information
    /// </summary>
    public class SubSample
    {
        public Guid SubSampleId { get; set; }

        public string Info { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Id: {SubSampleId} - Name: {Info}";
        }
    }

    public class SubSampleValidator : AbstractValidator<SubSample> {
        public SubSampleValidator() {
            RuleFor(x => x.SubSampleId)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator());
            RuleFor(x => x.Info).MaximumLength(128);
        }
    }
}
