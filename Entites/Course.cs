namespace ExaminationSystemDemo.Entites;

public class Course : AudtitableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Hours { get; set; }
    public int Degree { get; set; }
    public int InstructorId { get; set; }
    public bool IsDeleted { get; set; }
    public Instructor Instructor { get; set; } = default!;
    public List<Exam> Exams { get; set; } = [];
    public List<StudentCourse> StudentCourses { get; set; } = [];

}
