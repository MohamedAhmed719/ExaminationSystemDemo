using ExaminationSystemDemo.Contracts.Courses;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public interface ICourseService
{
    Task<Result<CourseResponse>> AddAsync(CourseRequest request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(string userId,int id, CourseRequest request, CancellationToken cancellationToken);
    Task<Result> ToggleStatusAsync(string userId,int id, CancellationToken cancellationToken);
}
