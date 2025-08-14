using ExaminationSystemDemo.Contracts.Questions;
using ExaminationSystemDemo.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystemDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionsController(IQuestionServce questionServce) : ControllerBase
    {
        private readonly IQuestionServce _questionServce = questionServce;

        [HttpPost("")]
        public async Task<IActionResult> Add([FromQuery]int examId,[FromBody] QuestionRequest request,CancellationToken cancellationToken)
        {   
            var result = await _questionServce.AddAsync(User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromQuery]int examId,int id,[FromBody] QuestionRequest request,CancellationToken cancellationToken)
        {
            var result = await _questionServce.UpdateAsync(id,User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpPut("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus(int id, [FromQuery] int examId,  CancellationToken cancellationToken)
        {
            var result = await _questionServce.ToggleStatusAsync(id, User.GetUserId()!, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}
