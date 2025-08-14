using ExaminationSystemDemo.Contracts.Questions;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public interface IQuestionServce
{
    Task<Result<QuestionResponse>> AddAsync(string userId, QuestionRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(int id, string userId, QuestionRequest request, CancellationToken cancellationToken);
    Task<Result> ToggleStatusAsync(int id, string userId, CancellationToken cancellationToken);
}
