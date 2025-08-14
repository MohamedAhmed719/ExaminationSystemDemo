using ExaminationSystemDemo.Contracts.Results;

namespace ExaminationSystemDemo.Services;

public interface IResultService
{
    Task<Result> EvaluateStudentResultAsync(string userId, EvaluateStudentResultRequest request, CancellationToken cancellationToken);
    Task<Result<StudentResultResponse>> GetAsync(string userId, StudentResultRequest request, CancellationToken cancellationToken);
}
