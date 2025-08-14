using ExaminationSystemDemo.Abstractions.Consts;
using ExaminationSystemDemo.Contracts.Results;
using ExaminationSystemDemo.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystemDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController(IResultService resultService) : ControllerBase
    {
        private readonly IResultService _resultService = resultService;

        [HttpPost("Evaluate")]
        [Authorize(Roles =DefaultRoles.Instructor)]
        public async Task<IActionResult> Evaluate([FromBody] EvaluateStudentResultRequest request,CancellationToken cancellationToken)
        {
            var result = await _resultService.EvaluateStudentResultAsync(User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }


        [HttpPost("")]
        [Authorize(Roles = DefaultRoles.Instructor + "," + DefaultRoles.Student)]
        public async Task<IActionResult> GetResult([FromBody] StudentResultRequest request,CancellationToken cancellationToken)
        {
            var result = await _resultService.GetAsync(User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }
}
