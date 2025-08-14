namespace ExaminationSystemDemo.Contracts.Questions;

public record QuestionRequest (
    string Content, 
    string QuestionLevel,
    int ExamId
    );