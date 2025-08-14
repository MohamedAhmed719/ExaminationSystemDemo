using ExaminationSystemDemo.Contracts.Courses;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public class CourseService(ApplicationDbContext context) : ICourseService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<CourseResponse>> AddAsync(CourseRequest request,CancellationToken cancellationToken)
    {
        var isCourseExists = await _context.Courses.AnyAsync(x => x.Name == request.Name,cancellationToken);

        if (isCourseExists)
            return Result.Failure<CourseResponse>(CourseErrors.DuplicatedCourse);

        var course = request.Adapt<Course>();

        await _context.AddAsync(course, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(course.Adapt<CourseResponse>());
    }

    public async Task<Result> UpdateAsync(string userId,int id,CourseRequest request,CancellationToken cancellationToken)
    {

        var isUserAllowedToUpdated = await _context.Courses.AnyAsync(x=>x.CreatedById == userId,cancellationToken);

        if (!isUserAllowedToUpdated)
            return Result.Failure(UserErrors.UserNotAllowed);

        var isCourseExists = await _context.Courses.AnyAsync(x => x.Name == request.Name && x.Id != id, cancellationToken);

        if (isCourseExists)
            return Result.Failure(CourseErrors.DuplicatedCourse);

        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (course is null)
            return Result.Failure(CourseErrors.CourseNotFound);

        course = request.Adapt(course);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> ToggleStatusAsync(string userId,int id,CancellationToken cancellationToken)
    { 
        var isUserAllowedToUpdated = await _context.Courses.AnyAsync(x => x.CreatedById == userId, cancellationToken);

        if (!isUserAllowedToUpdated)
            return Result.Failure(UserErrors.UserNotAllowed);
        var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (course is null)
            return Result.Failure(CourseErrors.CourseNotFound);

        course.IsDeleted = !course.IsDeleted;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }


}
