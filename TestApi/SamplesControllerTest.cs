using ApiTest.Services;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Api.Controllers;
using SampleApp.Application.Contracts.DTO;
using SampleApp.Application.Contracts.Services;
using SampleApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestApi
{
    public class SamplesControllerTest
    {
        SamplesController Controller;
        ISampleAppService Service;
        public SamplesControllerTest()
        {
            Service = new FakeSampleAppService();
            Controller = new SamplesController(Service);
        }
        [Fact]
        public void GetAllAsync_When_ReturnsOkResult()
        {
            //Act
            var okResult = Controller.GetAllAsync();

            //Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetAllAsync_When_ReturnsAllSamples() {
            //Act
            var okResult = Controller.GetAllAsync().Result as OkObjectResult;

            //Assert
            var samples = Assert.IsAssignableFrom<IEnumerable<SampleForRead>>(okResult.Value);
            Assert.Equal(10, samples.Count());
        }

        [Fact]
        public void GetByIdAsync_When_ReturnOkResult() {
            var existingGuid = new Guid("690eb11b-03f0-4799-ba20-c063958222a4");
            //Act
            var okResult = Controller.GetByIdAsync(existingGuid);

            //Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetByIdAsync_When_ReturnsSampleById() {
            var existingGuid = new Guid("690eb11b-03f0-4799-ba20-c063958222a4");

            //Act
            var okResult = Controller.GetByIdAsync(existingGuid).Result as OkObjectResult;

            //Assert
            var sample = Assert.IsType<SampleForRead>(okResult.Value);
            Assert.Equal(existingGuid, sample.Sample1Id);
        }

        [Fact]
        public void GetByIdAsync_When_ReturnsNotFound() {
            var nullGuid = Guid.NewGuid();

            //Act
            var notFoundResult = Controller.GetByIdAsync(nullGuid);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }
    }
}
