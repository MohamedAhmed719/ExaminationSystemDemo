using ExaminationSystemDemo.Abstractions.Consts;
using ExaminationSystemDemo.Contracts.Instructors;
using ExaminationSystemDemo.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystemDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =DefaultRoles.Instructor)]
    public class InstructorsController(IInstructorService instructorService) : ControllerBase
    {
        private readonly IInstructorService _instructorService = instructorService;

        [HttpPost("")]
        public async Task<IActionResult> Add([FromBody] InstructorRequest request,CancellationToken cancellationToken)
        {
            var result = await _instructorService.AddAsync(request, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }


        [HttpPut("enrollment/approve")]
        public async Task<IActionResult> ApproveEnrollment(EnrollmentApprovalRequest request, CancellationToken cancellationToken)
        {
            var result = await _instructorService.EnrollmentApprovalAsync(User.GetUserId()!, request.CourseId, request.StudentId, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpPut("enrollment/reject")]
        public async Task<IActionResult> RejectEnrollment(EnrollmentApprovalRequest request, CancellationToken cancellationToken)
        {
            var result = await _instructorService.RejectEnrollmentAsync(User.GetUserId()!, request.CourseId, request.StudentId, cancellationToken);

            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }
    }
}
