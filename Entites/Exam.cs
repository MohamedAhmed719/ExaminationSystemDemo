namespace ExaminationSystemDemo.Entites;

public class Exam : AudtitableEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public int Degree { get; set; } 
    public ExamType ExamType { get; set; } 
    public int NumberOfQuestions { get; set; }
    public bool IsDeleted { get; set; }
    public DateOnly StartsAt { get; set; } 
    public DateOnly EndsAt { get; set; }
    public Course Course { get; set; } = default!;
    public List<Question> Questions { get; set; } = [];
    public List<Choice> Choices { get; set; } = [];
    public List<StudentAnswer> StudentAnswer { get; set; } = [];
 }
