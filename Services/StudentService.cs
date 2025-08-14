using ExaminationSystemDemo.Contracts.Exams;
using ExaminationSystemDemo.Contracts.Students;
using System.Runtime.InteropServices;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public class StudentService(ApplicationDbContext context) : IStudentService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> AddStudentToCourseAsync(string userId,StudentCourseRequest request,CancellationToken cancellationToken)
    {

        var isCourseExists = await _context.Courses.AnyAsync(x => x.Id == request.CourseId, cancellationToken);

        if (!isCourseExists)
            return Result.Failure(CourseErrors.CourseNotFound);

        var isStudentEnrolled = await _context.StudentCourse.AnyAsync(x => x.CourseId == request.CourseId && x.Student.UserId == userId,cancellationToken);

        if (isStudentEnrolled)
            return Result.Failure(StudentErrors.DuplicatedStudentCourseEnrollment);

        var studentId = await _context.Students.Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefaultAsync(cancellationToken);

        var StudentCourse = request.Adapt<StudentCourse>();
        StudentCourse.StudentId = studentId;

        await _context.StudentCourse.AddAsync(StudentCourse, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
   
    public async Task<Result<StudentExamResponse>> TakeExam(string userId,StudentExamRequest request,CancellationToken cancellationToken)
    {
        var isExamExists = await _context.Exams.
            AnyAsync(
            x => !x.IsDeleted && x.Id == request.ExamId &&
            x.CourseId == request.courseId
            &&
            x.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow
            ) && x.EndsAt > DateOnly.FromDateTime(DateTime.UtcNow)
            ,cancellationToken
            );

        if (!isExamExists)
            return Result.Failure<StudentExamResponse>(ExamErrors.ExamNotFound);

        var isStudentAllowed = await _context.StudentCourse
            .AnyAsync(x => x.Student.UserId == userId && x.Status == StudentCourseStatus.Approved, cancellationToken);

        var response = await _context.Exams.Where(x => x.Id == request.ExamId)
            .Select(x => new StudentExamResponse(
                x.Name, x.ExamType, x.NumberOfQuestions, x.StartsAt, x.EndsAt, x.Questions
                .Select(x => new StudentExamQuestionResponse
                (x.Id, x.Content, x.QuestionLevel, x.Choices
                .Select(x => new Contracts.Choices.ChoiceResponse
                (x.Id, x.Content, x.QuestionId))
                .ToList())))).FirstOrDefaultAsync(cancellationToken);
        return Result.Success(response);
    }

    public async Task<Result> SaveStudentAnswersAsync(string userId,IEnumerable<StudentAnswerRequest> request,CancellationToken cancellationToken)
    {
        var studentAnswers = request.Adapt<List<StudentAnswer>>();
        var studentId = await _context.Students.Where(x => x.UserId == userId).Select(x => x.Id).FirstOrDefaultAsync(cancellationToken);


        studentAnswers.ForEach(x => x.StudentId = studentId);

        var x = studentAnswers;

        await _context.AddRangeAsync(studentAnswers);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
