using ExaminationSystemDemo.Contracts.Instructors;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public interface IInstructorService
{
    Task<Result<InstructorRespone>> AddAsync(InstructorRequest request, CancellationToken cancellationToken);
    Task<Result> EnrollmentApprovalAsync(string userId, int courseId, int studentId, CancellationToken cancellationToken);
    Task<Result> RejectEnrollmentAsync(string userId, int courseId, int studentId, CancellationToken cancellationToken);
}
