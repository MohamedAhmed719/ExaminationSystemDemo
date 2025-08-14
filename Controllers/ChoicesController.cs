using ExaminationSystemDemo.Contracts.Choices;
using ExaminationSystemDemo.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystemDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChoicesController(IChoiceService choiceService) : ControllerBase
    {
        private readonly IChoiceService _choiceService = choiceService;

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] ChoiceRequest request,CancellationToken cancellationToken)
        {
            var result = await _choiceService.AddAsync(User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ChoiceRequest request,CancellationToken cancellationToken)
        {
            var result = await _choiceService.UpdateAsync(id, User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpPut("{id}/toggle-status")]
        public async Task<IActionResult> ToggleSatus([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _choiceService.ToggleStatusAsync(id, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}
