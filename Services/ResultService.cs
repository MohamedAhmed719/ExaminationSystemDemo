using ExaminationSystemDemo.Contracts.Exams;
using ExaminationSystemDemo.Contracts.Results;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public class ResultService(ApplicationDbContext context) : IResultService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> EvaluateStudentResultAsync(string userId, EvaluateStudentResultRequest request, CancellationToken cancellationToken)
    {
        var isInsutrctorAllowedToEvaluate = await _context.Exams.AnyAsync(x => x.Course.Instructor.UserId == userId && x.Course.Id == request.CourseId
        && x.Id == request.ExamId, cancellationToken);

        if (!isInsutrctorAllowedToEvaluate)
            return Result.Failure(ExamErrors.InstructorNotAllowedToEvaluateExam);

        var correctAnswers = await _context.Choices.Where(x => x.IsCorrect && x.ExamId == request.ExamId).
            Include(x=> x.Question).ToListAsync(cancellationToken);

        var correctAnswerCount = 0;

        var studentAnswers = await _context.StudentAnswer.Where(x => x.StudentId == request.StudentId && x.ExamId == request.ExamId)
            .ToListAsync(cancellationToken);

        foreach (var answer in correctAnswers)
        {
            foreach (var studentAnswer in studentAnswers)
            {
                if (studentAnswer.ChoiceId == answer.Id)
                {
                    correctAnswerCount += answer.Question.Score;
                    break;
                }        
            }
        }

        var ExamDegree = await _context.Exams.Where(x => x.Id == request.ExamId).Select(x=>x.Degree).FirstOrDefaultAsync( cancellationToken);

        var result = new StudentResult { StudentId = request.StudentId, Degree = correctAnswerCount, ExamId = request.ExamId, IsPass = correctAnswerCount >= (double)ExamDegree /2 };
        await _context.StudentResults.AddAsync(result, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result<StudentResultResponse>> GetAsync(string userId,StudentResultRequest request,CancellationToken cancellationToken)
    {

        var isUserAllowed = await _context.StudentResults.AnyAsync(x => x.StudentId == request.studentId && x.Student.UserId == userId, cancellationToken);

        if (!isUserAllowed)
            return Result.Failure<StudentResultResponse>(ResultErrors.StudentNotAllowedToGetResult);

        var result = await _context.StudentResults.Where(x => x.ExamId == request.ExamId && x.Student.Id == request.studentId)
            .Select(x => new StudentResultResponse(x.Id, x.Degree, x.IsPass, x.Student.Id, x.Student.Grade, x.Exam.Course.Name, x.Student.Name))
            .FirstOrDefaultAsync(cancellationToken);

        if (result is null)
            return Result.Failure<StudentResultResponse>(ResultErrors.ResultNotFound);

        return Result.Success(result);

    }
}
