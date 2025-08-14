using ExaminationSystemDemo.Contracts.Exams;
using ExaminationSystemDemo.Extensions;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public class ExamService(ApplicationDbContext context) : IExamService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<ExamResponse>> AddAsync(string userId,ExamRequest request,CancellationToken cancellationToken)
    {
        var isUserAllowedToCreateExam = await _context.Courses.AnyAsync(x => x.CreatedById == userId, cancellationToken);

        if (!isUserAllowedToCreateExam)
            return Result.Failure<ExamResponse>(UserErrors.UserNotAllowed);

        var exam = request.Adapt<Exam>();

        await _context.AddAsync(exam, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(exam.Adapt<ExamResponse>());
    }

    public async Task<Result> UpdateAsync(string userId,int id,ExamRequest request,CancellationToken cancellationToken)
    {
        var isUserAllowedToCreateExam = await _context.Courses.AnyAsync(x => x.CreatedById == userId, cancellationToken);

        if (!isUserAllowedToCreateExam)
            return Result.Failure<ExamResponse>(UserErrors.UserNotAllowed);

        var exam = await _context.Exams.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (exam is null)
            return Result.Failure(ExamErrors.ExamNotFound);

        exam = request.Adapt(exam);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> ToggleStatusAsync(string userId, int id, CancellationToken cancellationToken)
    {
        var isUserAllowedToCreateExam = await _context.Courses.AnyAsync(x => x.CreatedById == userId, cancellationToken);

        if (!isUserAllowedToCreateExam)
            return Result.Failure<ExamResponse>(UserErrors.UserNotAllowed);

        var exam = await _context.Exams.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (exam is null)
            return Result.Failure(ExamErrors.ExamNotFound);

        exam.IsDeleted = !exam.IsDeleted;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
