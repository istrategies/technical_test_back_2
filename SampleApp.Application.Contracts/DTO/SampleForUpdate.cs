using FluentValidation;
using SampleApp.Application.Contracts.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleApp.Application.Contracts.DTO
{
    /// <summary>
    /// DTO to update an existing Sample
    /// </summary>
    public class SampleForUpdate
    {
        public Guid SampleId { get; set; }

        public string Name { get; set; }

        public List<SubSample> SubSamples { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Id: {SampleId} - Name: {Name} - SubSample: {SubSamples.Count} items.";
        }
    }

    public class SampleForUpdateValidator : AbstractValidator<SampleForUpdate> {
        public SampleForUpdateValidator() {
            RuleFor(x => x.SampleId)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator());
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);
            RuleForEach(x => x.SubSamples)
                .SetValidator(new SubSampleValidator());
        }
    }
}
