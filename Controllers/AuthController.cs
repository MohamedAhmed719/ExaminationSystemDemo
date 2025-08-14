using ExaminationSystemDemo.Contracts.Authentiaction;
using ExaminationSystemDemo.Contracts.Instructors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystemDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var authResult = await _authService.GetJwtAsync(request.Email, request.Password);

            return authResult.IsSuccess ? Ok(authResult.Value) : BadRequest(authResult.Error);
        }

        [HttpPost("student/register")]
        public async Task<IActionResult> StudentRegister([FromBody] StudentRegisterRequest request,CancellationToken cancellationToken)
        {
            var result = await _authService.StudentRegisterAsync(request, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }


        [HttpPost("instructor/register")]
        public async Task<IActionResult> InstructorRegister([FromBody] InstructorRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.InstructorRegisterAsync(request, cancellationToken);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
