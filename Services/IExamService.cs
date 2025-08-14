using ExaminationSystemDemo.Contracts.Exams;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public interface IExamService
{
    Task<Result<ExamResponse>> AddAsync(string userId,ExamRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(string userId, int id, ExamRequest request, CancellationToken cancellationToken);
    Task<Result> ToggleStatusAsync(string userId, int id, CancellationToken cancellationToken);
}
