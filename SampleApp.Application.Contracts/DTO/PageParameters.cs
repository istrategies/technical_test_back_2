using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Application.Contracts.DTO
{
    public class PageParameters
    {
        [BindRequired]
        public int pageNumber { get; set; }
        [BindRequired]
        public int pageSize { get; set; }
    }

    public class PageParemetersValidator : AbstractValidator<PageParameters> {
        public PageParemetersValidator() {
            RuleFor(x => x.pageSize).LessThan(101);
        }
    }
}
