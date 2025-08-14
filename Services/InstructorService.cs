using ExaminationSystemDemo.Contracts.Instructors;
using Microsoft.Identity.Client.Extensions.Msal;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public class InstructorService(ApplicationDbContext context) : IInstructorService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<InstructorRespone>> AddAsync(InstructorRequest request,CancellationToken cancellationToken)
    {
        var instructor = request.Adapt<Instructor>();

        await _context.AddAsync(instructor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(instructor.Adapt<InstructorRespone>());
    }


    public async Task<Result> EnrollmentApprovalAsync(string userId,int courseId,int studentId,CancellationToken cancellationToken)
    {
        var isInstructorAllowed = await _context.StudentCourse.Where(x => x.Course.Id == courseId && x.Course.CreatedById == userId)
            .AnyAsync(cancellationToken);

        if (!isInstructorAllowed)
            return Result.Failure(CourseErrors.InstructorNotAllowedToApprove);

        var studentCourse = await _context
            .StudentCourse
            .FirstOrDefaultAsync(x => x.CourseId == courseId && x.StudentId == studentId, cancellationToken);

        if (studentCourse is null)
            return Result.Failure(CourseErrors.EnrollmentNotFound);

        if (studentCourse.Status == StudentCourseStatus.Approved)
            return Result.Failure(CourseErrors.DuplicatedEnrollmentApproval);

        studentCourse.Status = StudentCourseStatus.Approved;

        studentCourse.ApprovalDate = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> RejectEnrollmentAsync(string userId,int courseId,int studentId,CancellationToken cancellationToken)
    {
        var isInstructorAllowed = await _context.StudentCourse.Where(x => x.Course.Id == courseId && x.Course.CreatedById == userId)
            .AnyAsync(cancellationToken);

        if (!isInstructorAllowed)
            return Result.Failure(CourseErrors.InstructorNotAllowedToReject);

        var studentCourse = await _context.StudentCourse.FirstOrDefaultAsync(x => x.CourseId == courseId && x.StudentId == studentId, cancellationToken);

        if (studentCourse is null)
            return Result.Failure(CourseErrors.EnrollmentNotFound);

        studentCourse.Status = StudentCourseStatus.Rejected;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
