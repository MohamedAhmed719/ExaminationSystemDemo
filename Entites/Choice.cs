namespace ExaminationSystemDemo.Entites;

public class Choice 
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public int QuestionId { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsCorrect { get; set; } 
    public int ExamId { get; set; }
    public Question Question { get; set; } = default!;
    public Exam Exam { get; set; } = default!;
    public List<StudentAnswer> StudentAnswers { get; set; } = default!;

}
