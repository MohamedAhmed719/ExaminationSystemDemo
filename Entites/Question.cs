namespace ExaminationSystemDemo.Entites;

public class Question : AudtitableEntity
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public int ExamId { get; set; }
    public string QuestionLevel { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public int Score { get; set; }
    public Exam Exam { get; set; } = default!;
    public List<Choice> Choices { get; set; } = [];
    public List<StudentAnswer> StudentAnswers { get; set; } = [];

}

