using ExaminationSystemDemo.Contracts.Choices;
using System.Runtime.InteropServices;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public class ChoiceService(ApplicationDbContext context) : IChoiceService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<ChoiceResponse>> AddAsync(string userId,ChoiceRequest request,CancellationToken cancellationToken)
    {
        var isQuestionExists = await _context.Questions.AnyAsync(x => x.Id == request.QuestionId && x.CreatedById == userId, cancellationToken);

        if (!isQuestionExists)
            return Result.Failure<ChoiceResponse>(QuestionErrors.QuestionNotFound);

        var isChoiceExists = await _context.Choices.AnyAsync(x => x.Content == request.Content && x.QuestionId == request.QuestionId,cancellationToken);

        if (isChoiceExists)
            return Result.Failure<ChoiceResponse>(ChoiceErrors.DuplicatedChoice);

        var choice = request.Adapt<Choice>();

        await _context.AddAsync(choice, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(choice.Adapt<ChoiceResponse>());
    }

    public async Task<Result> UpdateAsync(int id,string userId,ChoiceRequest request,CancellationToken cancellationToken)
    {
        var isQuestionExists = await _context.Questions.AnyAsync(x => x.Id == request.QuestionId && x.CreatedById == userId, cancellationToken);

        if (!isQuestionExists)
            return Result.Failure<ChoiceResponse>(QuestionErrors.QuestionNotFound);

        var choice = await _context.Choices.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (choice is null)
            return Result.Failure(ChoiceErrors.ChoiceNotFound);

        choice = request.Adapt(choice);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> ToggleStatusAsync(int id,CancellationToken cancellationToken)
    {
        var choice = await _context.Choices.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (choice is null)
            return Result.Failure(ChoiceErrors.ChoiceNotFound);


        choice.IsDeleted = !choice.IsDeleted;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
