
namespace ExaminationSystemDemo.Entites;

public class StudentCourse
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }
    public int? Degree { get; set; }
    public StudentCourseStatus Status { get; set; } = StudentCourseStatus.Pending;
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    public DateTime? ApprovalDate { get; set; }
    public Student Student { get; set; } = default!;
    public Course Course { get; set; } = default!;
}
