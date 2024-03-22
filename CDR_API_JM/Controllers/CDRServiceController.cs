using Application;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CDR_API_JM.Models;
using System.ComponentModel.DataAnnotations;

namespace CDR_API_JM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CDRController : ControllerBase
    {
        private readonly ICDRService _cdrService;

        public CDRController(ICDRService cdrService)
        {
            _cdrService = cdrService;
        }

        [HttpGet("{reference}")]
        [SwaggerOperation(Summary = "Retrieve individual CDR by the CDR Reference",
                          Description = "Retrieves detailed information about a Call Detail Record (CDR) based on its unique reference.",
                          OperationId = "GetByReference",
                          Tags = new[] { "CDR" })]
        public async Task<ActionResult<SuccessResponse<CDR>>> GetByReference(string reference)
        {
            var cdr = await _cdrService.GetByReferenceAsync(reference);
            if (cdr == null)
            {
                return NotFound(new ErrorResponse { ErrorMessage = "CDR not found" });
            }
            return new SuccessResponse<CDR> { Data = cdr };
        }

        [HttpGet("calls/count")]
        [SwaggerOperation(Summary = "Retrieve call count and total duration",
                          Description = "Retrieves the count and total duration of all calls in a specified time period.",
                          OperationId = "GetCallCountAndTotalDuration",
                          Tags = new[] { "CDR" })]
        public async Task<ActionResult<SuccessResponse<Dictionary<string, object>>>> GetCallCountAndTotalDuration([FromQuery] GetCallCountAndTotalDurationRequest request)
        {
            try
            {
                var result = await _cdrService.GetCallCountAndTotalDurationAsync(request.StartDate, request.EndDate);
                return new SuccessResponse<Dictionary<string, object>> { Data = result };
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("calls/caller")]
        [SwaggerOperation(Summary = "Retrieve all CDRs for a specific Caller ID",
                          Description = "Retrieves all Call Detail Records (CDRs) for a specific Caller ID within a specified time period.",
                          OperationId = "GetCdrsByCallerId",
                          Tags = new[] { "CDR" })]
        public async Task<ActionResult<SuccessResponse<IEnumerable<CDR>>>> GetCdrsByCallerId([FromQuery] GetCdrsByCallerIdRequest request)
        {
            try
            {
                var cdrs = await _cdrService.GetCdrsByCallerIdAsync(request.CallerId, request.StartDate, request.EndDate);
                return new SuccessResponse<IEnumerable<CDR>> { Data = cdrs };
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { ErrorMessage = ex.Message });
            }
        }

        [HttpGet("calls/expensive")]
        [SwaggerOperation(Summary = "Retrieve N most expensive calls",
                          Description = "Retrieves the N most expensive calls for a specific Caller ID within a specified time period.",
                          OperationId = "GetMostExpensiveCalls",
                          Tags = new[] { "CDR" })]
        public async Task<ActionResult<SuccessResponse<IEnumerable<CDR>>>> GetMostExpensiveCalls([FromQuery] GetMostExpensiveCallsRequest request)
        {
            try
            {
                var cdrs = await _cdrService.GetMostExpensiveCallsAsync(request.CallerId, request.StartDate, request.EndDate, request.Count);
                return new SuccessResponse<IEnumerable<CDR>> { Data = cdrs };
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse { ErrorMessage = ex.Message });
            }
        }
    }
}