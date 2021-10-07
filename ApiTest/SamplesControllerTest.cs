using ApiTest.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SampleApp.Api.Controllers;
using SampleApp.Application.Contracts.Services;
using Xunit;

namespace ApiTest
{
    public class Tests
    {
        SamplesController Controller;
        ISampleAppService Service;
        [SetUp]
        public void Setup()
        {
            Service = new FakeSampleAppService();
            Controller = new SamplesController(Service);
        }

        [Fact]
        public void GetAllAsync_When_ReturnsOkResult()
        {
            // Act
            var okResult = Controller.GetAllAsync();
            // Assert
            Assert.IsType<OkObjectResult>
        }
    }
}