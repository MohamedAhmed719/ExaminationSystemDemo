namespace ExaminationSystemDemo.Entites;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Grade { get; set; }
    public string UserId { get; set; }
    public List<Exam> Exams { get; set; } = [];
    public List<StudentResult> Results { get; set; } = [];
    public List<StudentCourse> StudentCourses { get; set; } = [];

    public ApplicationUser User { get; set; } = default!;

    public List<StudentAnswer> StudentAnswers { get; set; } = [];

}
