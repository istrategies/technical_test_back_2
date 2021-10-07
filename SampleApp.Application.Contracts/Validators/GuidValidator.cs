using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Application.Contracts.Validators
{
    class GuidValidator : AbstractValidator<Guid>
    {
        public GuidValidator() {
            RuleFor(x => x).NotEmpty();
        }
    }
}
