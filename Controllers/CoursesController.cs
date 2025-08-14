using ExaminationSystemDemo.Abstractions.Consts;
using ExaminationSystemDemo.Contracts.Courses;
using ExaminationSystemDemo.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystemDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =DefaultRoles.Instructor)]
    public class CoursesController(ICourseService courseService ) : ControllerBase
    {
        private readonly ICourseService _courseService = courseService;

        [HttpPost("")]
        
        public async Task<IActionResult> Add([FromBody] CourseRequest request,CancellationToken cancellationToken)
        {
            var result = await _courseService.AddAsync(request, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CourseRequest request,CancellationToken cancellationToken)
        {
            var result = await _courseService.UpdateAsync(User.GetUserId()!,id, request, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpPut("{id}/toggle-status")]
        public async Task<IActionResult> ToggleStatus([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _courseService.ToggleStatusAsync(User.GetUserId()!,id, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}
