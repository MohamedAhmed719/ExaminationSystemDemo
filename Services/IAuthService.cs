using ExaminationSystemDemo.Contracts.Authentiaction;
using ExaminationSystemDemo.Contracts.Instructors;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public interface IAuthService
{
    Task<Result<AuthResponse>> GetJwtAsync(string email, string password);
    Task<Result> StudentRegisterAsync(StudentRegisterRequest request, CancellationToken cancellationToken);
    Task<Result> InstructorRegisterAsync(InstructorRequest request, CancellationToken cancellationToken);

}
