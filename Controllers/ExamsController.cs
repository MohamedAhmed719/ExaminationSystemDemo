using ExaminationSystemDemo.Abstractions.Consts;
using ExaminationSystemDemo.Contracts.Exams;
using ExaminationSystemDemo.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystemDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =DefaultRoles.Instructor)]
    public class ExamsController(IExamService examService) : ControllerBase
    {
        private readonly IExamService _examService = examService;

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] ExamRequest request,CancellationToken cancellationToken)
        {
            var result = await _examService.AddAsync(User.GetUserId()!,request, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody] ExamRequest request, CancellationToken cancellationToken)
        {
            var result = await _examService.UpdateAsync(User.GetUserId()!,id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpPost("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _examService.ToggleStatusAsync(User.GetUserId()!, id, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}
