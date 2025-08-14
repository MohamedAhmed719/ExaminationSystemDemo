using ExaminationSystemDemo.Contracts.Questions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public class QuestionService(ApplicationDbContext context) : IQuestionServce
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<QuestionResponse>> AddAsync(string userId,QuestionRequest request,CancellationToken cancellationToken)
    {
        var isExamExists = await _context.Exams.AnyAsync(x => x.Id == request.ExamId, cancellationToken);

        if (!isExamExists)
            return Result.Failure<QuestionResponse>(ExamErrors.ExamNotFound);

        var isUserAllowedToCreateQuestion = await _context.Exams.AnyAsync(x => x.Id == request.ExamId && x.CreatedById == userId, cancellationToken);

        if (!isUserAllowedToCreateQuestion)
            return Result.Failure<QuestionResponse>(ExamErrors.InstructorNotAllowed);

        var question = request.Adapt<Question>();

        await _context.AddAsync(question,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(question.Adapt<QuestionResponse>());
    }
 
    public async Task<Result> UpdateAsync(int id,string userId,QuestionRequest request,CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == id && x.CreatedById == userId,cancellationToken);

        if (question is null)
            return Result.Failure(QuestionErrors.QuestionNotFound);

        question = request.Adapt(question);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> ToggleStatusAsync(int id,string userId,CancellationToken cancellationToken)
    {
        var question = await _context.Questions.FirstOrDefaultAsync(x => x.Id == id && x.CreatedById == userId, cancellationToken);

        if (question is null)
            return Result.Failure(QuestionErrors.QuestionNotFound);

        question.IsDeleted = !question.IsDeleted;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
