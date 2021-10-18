using Moq;
using NUnit.Framework;
using SampleApp.Application.Services;
using SampleApp.Domain.Entities;
using SampleApp.Infrastructure.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleApp.Application.UnitTests.Tests
{
    class WhenSampleServiceIsCalled
    {
        private static SampleAppService _sut;

        private static Mock<ISampleAppRepository> _sampleAppRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _sampleAppRepositoryMock = new Mock<ISampleAppRepository>();

            _sut = new SampleAppService(_sampleAppRepositoryMock.Object);
        }

        [Test]
        public async Task Given_ValidGuid_When_GetByIdIsCalled_ReturnTheExistenRegisterMapped()
        {
            var guidToTest = Guid.NewGuid();
            var sampleResponse = BuildSample(guidToTest);

            _sampleAppRepositoryMock.Setup(s
                => s.GetByIdAsync(It.Is<Guid>(x
                => x == guidToTest)))
                .Returns(Task.FromResult(sampleResponse));

            var response = await _sut.GetByIdAsync(guidToTest);

            Assert.NotNull(response);
            Assert.IsNull(response.SampleId);
            Assert.AreEqual(response.Name, response.Name);
        }

        private Sample BuildSample(Guid guid)
        {
            return new Sample
            {
                SampleId = guid,
                Created = DateTime.Now,
                Name = "NameTests",
                SubSamples = new List<SubSample>
                {
                    new SubSample
                    {
                        Info = "InfoTest",
                        SubSampleId = guid,
                        SampleId = guid
                    }
                }
            };
        }
    }
}
