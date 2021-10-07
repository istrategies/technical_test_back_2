using AutoMapper;
using FluentValidation;
using SampleApp.Application.Contracts.Validators;
using SampleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleApp.Application.Contracts.DTO
{
    /// <summary>
    /// DTO for the Sample creation
    /// </summary>
    public class SampleForCreate
    {
        public Guid SampleId { get; set; }

        public string Name { get; set; }

        public List<SubSample> SubSamples { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Name: {Name} - SubSample: {SubSamples.Count()} items.";
        }

    }

    public class SampleForCreateValidator : AbstractValidator<SampleForCreate> {
        public SampleForCreateValidator() {
            RuleFor(x => x.SampleId)
                .NotNull()
                .NotEmpty()
                .SetValidator(new GuidValidator());
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30);
            RuleForEach(x => x.SubSamples)
                .SetValidator(new SubSampleValidator());
        }
    }
}
