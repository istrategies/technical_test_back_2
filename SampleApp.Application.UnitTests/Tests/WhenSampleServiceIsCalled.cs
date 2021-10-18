using AutoMapper;
using Moq;
using NUnit.Framework;
using SampleApp.Application.Contracts.DTO;
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
        private static Mock<IMapper> _mapperMock;

        [SetUp]
        public void Setup()
        {
            _sampleAppRepositoryMock = new Mock<ISampleAppRepository>();
            _mapperMock = new Mock<IMapper>();

            _sut = new SampleAppService(_sampleAppRepositoryMock.Object);
        }

        [Test]
        public async Task Given_ValidGuid_When_GetByIdIsCalled_ReturnTheExistenRegisterMapped()
        {
            var guidToTest = Guid.NewGuid();
            var sampleResponse = BuildSample(guidToTest);
            var sampleResponseToRead = BuildSampleToRead(guidToTest);

            _sampleAppRepositoryMock.Setup(s
                => s.GetByIdAsync(It.Is<Guid>(x
                => x == guidToTest)))
                .Returns(Task.FromResult(sampleResponse));

            _mapperMock.Setup(m => m.Map<SampleForRead>(It.IsAny<Sample>()))
                .Returns(sampleResponseToRead);

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
                SubSamples = new List<Domain.Entities.SubSample>
                {
                    new Domain.Entities.SubSample
                    {
                        Info = "InfoTest",
                        SubSampleId = guid,
                        SampleId = guid
                    }
                }
            };
        }

        private SampleForRead BuildSampleToRead(Guid guid)
        {
            return new SampleForRead
            {
                SampleId = null,
                Created = DateTime.Now,
                Name = "NameTests",
                SubSamples = new List<Contracts.DTO.SubSample>
                {
                    new Contracts.DTO.SubSample
                    {
                        Info = "InfoTest",
                        SubSampleId = guid
                    }
                }
            };
        }
    }
}
