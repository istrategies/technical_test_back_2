using SampleApp.Application.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleApp.Application.Contracts.Services
{
    /// <summary>
    /// ISample1AppService
    /// </summary>
    public interface ISampleAppService : IDisposable
    {
        /// <summary>
        /// Retrieve all Samples
        /// </summary>
        /// <param name="numPage"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<SampleForRead>> GetAllSamplesAsync(int rowsNumber = 0, string key = "Name");

        /// <summary>
        /// Retrieves a sample based opn it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SampleForRead> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all subsamples with it's sample data
        /// </summary>
        /// <param name="pageParameters"></param>
        /// <param name="greaterThan"></param>
        /// <param name="lessThan"></param>
        /// <returns></returns>
        Task<IEnumerable<SubSampleForRead>> GetAllSubSamplesAsync(PageParameters pageParameters, DateTimeOffset? greaterThan, DateTimeOffset? lessThan);

        /// <summary>
        /// Retrieves the SubSamples of a Sample
        /// </summary>
        /// <param name="sampleId"></param>
        /// <returns></returns>
        Task<IEnumerable<Contracts.DTO.SubSample>> GetSubSamplesAsync(Guid sampleId);

        /// <summary>
        /// Adds a new Sample
        /// </summary>
        /// <param name="sampleForCreation"></param>
        /// <returns></returns>
        Task<SampleForRead> AddSampleAsync(SampleForCreate sampleForCreation);

        /// <summary>
        /// Updates an existing Sample
        /// </summary>
        /// <param name="sampleForCreation"></param>
        /// <returns></returns>
        Task<SampleForRead> UpdateSampleAsync(SampleForUpdate sampleForUpdate);

        /// <summary>
        /// Deletes an existing Sample
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteSampleAsync(Guid id);
    }
}
