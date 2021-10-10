using FluentValidation.TestHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using SampleApp.Api.Controllers;
using SampleApp.Application.Contracts.DTO;
using SampleApp.Application.Contracts.Services;
using SampleApp.Application.Contracts.Validators;
using SampleApp.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using UnitTest.Mock;

namespace UnitTest
{
    [TestFixture(TestName = "Sample Controller Test")]
    public class UnitTest
    {
        private IServiceProvider _serviceProvider;
        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();

            SampleApp.Application.Contracts.Startup.AddContractsLayer(serviceCollection);

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>() { { "maxRows", "1000" } })
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddSingleton<ISampleAppService, SampleAppService>();
            serviceCollection.AddSingleton<SampleFilterValidator>();
            serviceCollection.AddSingleton<SampleContext, MockSampleContext>();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        [Test(Description = "GetAllAsync Test")]
        [TestCase("{\"StartCreated\":\"2021-01-01T00:00:00\", \"EndCreated\":\"2021-01-31T00:00:00\", \"CurrentPage\":1, \"Rows\": 20}", TestName = "Data Return")]
        [TestCase("{\"StartCreated\":\"2021-01-01T00:00:00\", \"EndCreated\":\"2021-01-31T00:00:00\", \"CurrentPage\":50000, \"Rows\": 20}", TestName = "Data Page Out")]
        [TestCase("{\"StartCreated\":\"2050-01-01T00:00:00\", \"EndCreated\":\"2050-01-31T00:00:00\", \"CurrentPage\":1, \"Rows\": 20}", TestName = "Data Filter Out")]
        public async Task Test_GetAllAsync(string filter)
        {
            var service = _serviceProvider.GetRequiredService<ISampleAppService>();
            var controller = new SamplesController(service);
            var sampleFilter = JsonSerializer.Deserialize<SampleFilter>(filter);
            var result = await controller.GetAllAsync(sampleFilter) as Microsoft.AspNetCore.Mvc.OkObjectResult;

            Assert.NotNull(result.Value);
        }

        [Test(Description = "GetAllAsync Validations Test")]
        [TestCase("{}", TestName = "Empty Data Validations")]
        [TestCase("{\"CurrentPage\":-1, \"Rows\": -1}", TestName = "Negative Row and Page Validation")]
        [TestCase("{\"CurrentPage\":1, \"Rows\": 0}", TestName = "Less than 1 Row")]
        [TestCase("{\"CurrentPage\":0, \"Rows\": 1}", TestName = "Less than 1 Page")]
        [TestCase("{\"StartCreated\":\"2100-01-01T00:00:00\", \"EndCreated\":\"2000-01-31T00:00:00\"}", TestName = "Date Less Validation")]
        public void Test_GetAllAsync_Validations(string filter)
        {
            var validator = _serviceProvider.GetRequiredService<SampleFilterValidator>();
            var sampleFilter = JsonSerializer.Deserialize<SampleFilter>(filter);

            var result = validator.TestValidate(sampleFilter);

            if (sampleFilter.CurrentPage < 1)
                result.ShouldHaveValidationErrorFor(x => x.CurrentPage);
            else
                result.ShouldNotHaveValidationErrorFor(x => x.CurrentPage);

            if (sampleFilter.Rows < 1)
                result.ShouldHaveValidationErrorFor(x => x.Rows);
            else
                result.ShouldNotHaveValidationErrorFor(x => x.Rows);

            result.ShouldHaveValidationErrorFor(x => x.EndCreated);

            if (sampleFilter.StartCreated == DateTimeOffset.MinValue)
                result.ShouldHaveValidationErrorFor(x => x.StartCreated);
        }
    }
}