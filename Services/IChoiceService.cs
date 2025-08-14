using ExaminationSystemDemo.Contracts.Choices;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public interface IChoiceService
{
    Task<Result<ChoiceResponse>> AddAsync(string userId, ChoiceRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(int id, string userId, ChoiceRequest request, CancellationToken cancellationToken);
    Task<Result> ToggleStatusAsync(int id, CancellationToken cancellationToken);
}
