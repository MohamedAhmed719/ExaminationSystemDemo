using ExaminationSystemDemo.Abstractions.Consts;
using ExaminationSystemDemo.Contracts.Exams;
using ExaminationSystemDemo.Contracts.Students;
using ExaminationSystemDemo.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystemDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =DefaultRoles.Student)]
    public class StudentsController(IStudentService studentService) : ControllerBase
    {
        private readonly IStudentService _studentService = studentService;
        [HttpPost("")]
        public async Task<IActionResult> AddStudentCourse([FromBody] StudentCourseRequest request, CancellationToken cancellationToken)
        {
            var result = await _studentService.AddStudentToCourseAsync(User.GetUserId()!, request, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [HttpPost("Exam")]
        public async Task<IActionResult> TakeExam([FromBody] StudentExamRequest request,CancellationToken cancellationToken)
        {
            var result = await _studentService.TakeExam(User.GetUserId(), request, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost("Exam/Save")]
        public async Task<IActionResult> SaveStudentAnswers([FromBody] IEnumerable<StudentAnswerRequest> request, CancellationToken cancellationToken)
        {
            var result = await _studentService.SaveStudentAnswersAsync(User.GetUserId()!,request, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
