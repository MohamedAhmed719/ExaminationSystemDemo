namespace ExaminationSystemDemo.Entites;

public class StudentAnswer
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int QuestionId { get; set; }
    public int ChoiceId { get; set; }
    public int ?ExamId { get; set; }

    public Student Student { get; set; } = default!;
    public Question Question { get; set; } = default!;
    public Choice Choice { get; set; } = default!;
    public Exam Exam { get; set; } = default!;
}
