using ExaminationSystemDemo.Contracts.Exams;
using ExaminationSystemDemo.Contracts.Instructors;
using ExaminationSystemDemo.Contracts.Students;
using Result = ExaminationSystemDemo.Abstractions.Result;

namespace ExaminationSystemDemo.Services;

public interface IStudentService
{
    Task<Result> AddStudentToCourseAsync(string userId, StudentCourseRequest request, CancellationToken cancellationToken);
    Task<Result<StudentExamResponse>> TakeExam(string userId, StudentExamRequest request, CancellationToken cancellationToken);
    Task<Result> SaveStudentAnswersAsync(string userId, IEnumerable<StudentAnswerRequest> request, CancellationToken cancellationToken);
}
