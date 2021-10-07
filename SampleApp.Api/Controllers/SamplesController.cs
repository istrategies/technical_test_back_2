using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleApp.Application.Contracts.DTO;
using SampleApp.Application.Contracts.Services;
using SampleApp.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Api.Controllers
{
    /// <summary>
    /// Controller to perform operations with Sample entity
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        private readonly ISampleAppService _service;
        private readonly ILogger<SamplesController> _logger;

        public IRequestLogger RequestLogger { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">Samples service with its business logic</param>
        /// <param name="logger"></param>
        /// <param name="requestLogger"></param>
        public SamplesController(ISampleAppService service, ILogger<SamplesController> logger, IRequestLogger requestLogger)
        {
            _service = service;
            _logger = logger;
            RequestLogger = requestLogger;
        }

        /// <summary>
        /// Retrieves all the Sample entities WITHOUT its subsamples
        /// </summary>   
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<SampleForRead>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            RequestLogger.Log("(SampleApi) GetAllAsync Request", HttpContext);

            IEnumerable<SampleForRead> result;

            using (_service)
                result = await _service.GetAllSamplesAsync(10, "Created");

            return Ok(result);
        }

        /// <summary>
        /// Retrieves the given Sample WITH all its SubSamples
        /// </summary>
        /// <param name="id">Sample Id to retrieve</param>        
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(SampleForRead), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            RequestLogger.Log("(SampleApi) GetByIdAsync Request", HttpContext);

            SampleForRead result;

            using (_service)
                result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Retrieves all SubSamples with it's Samples data
        /// </summary>
        /// <param name="pageParameters"></param>
        /// <param name="greaterThan"></param>
        /// <param name="lessThan"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/subsamples")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<SubSampleForRead>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSubSamplesAsync([FromQuery] PageParameters pageParameters,
            DateTimeOffset greaterThan,
            DateTimeOffset lessThan)
        {
            RequestLogger.Log("(SampleApi) GetAllSubSamplesAsync Request", HttpContext);

            IEnumerable<SubSampleForRead> result;

            using (_service)
                result = await _service.GetAllSubSamplesAsync(pageParameters, greaterThan, lessThan);

            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.UNEXPECTED_ERROR);

            return Ok(result);
        }

        /// <summary>
        /// Retrieves only the SubSamples from a given Sample
        /// </summary>
        /// <param name="id">Sample Id to retrieve its samples</param>
        [HttpGet]
        [Route("{id}/subsamples")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<SubSample>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubSamplesAsync(Guid id)
        {
            RequestLogger.Log("(SampleApi) GetSubSamplesAsync Request", HttpContext);

            IEnumerable<SubSample> result;

            using (_service)
                result = await _service.GetSubSamplesAsync(id);

            // Just in case, this scenario should not happen
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.UNEXPECTED_ERROR);

            return Ok(result);
        }

        /// <summary>
        /// Saves a new Sample entity
        /// </summary>
        /// <param name="sample">Sample to create</param>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(SampleForRead), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateAsync([FromBody] SampleForCreate sample)
        {
            RequestLogger.Log("(SampleApi) CreateAsync Request", HttpContext);

            SampleForRead result;

            using (_service)
            {
                result = await _service.AddSampleAsync(sample);
            }

            // Just in case, this scenario should not happen
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.UNEXPECTED_ERROR);

            return Created(string.Empty, result);
        }

        /// <summary>
        /// Updates an existing Sample
        /// </summary>
        /// <param name="sample">Sample to update</param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(SampleForRead), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync(SampleForUpdate sample)
        {
            RequestLogger.Log("(SampleApi) UpdateAsync Request", HttpContext);

            SampleForRead result;

            using (_service)
                result = await _service.UpdateSampleAsync(sample);

            // Just in case, this scenario should not happen
            if (result == null)
                return StatusCode(StatusCodes.Status500InternalServerError, Constants.UNEXPECTED_ERROR);

            return Ok(result);
        }

        /// <summary>
        /// Removes an existing Sample from the system
        /// </summary>
        /// <param name="id">Sample Id to remove.</param>
        [HttpPost]
        [Route("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            RequestLogger.Log("(SampleApi) DeleteAsync Request", HttpContext);

            using (_service)
                await _service.DeleteSampleAsync(id);

            return NoContent();
        }
    }
}
