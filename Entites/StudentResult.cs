namespace ExaminationSystemDemo.Entites;

public class StudentResult
{
    public int Id { get; set; }
    public int Degree { get; set; } 
    public bool IsPass { get; set; }
    public int StudentId { get; set; }
    public int ExamId { get; set; }
    public Student Student { get; set; } = default!;
    public Exam Exam { get; set; } = default!;
}
