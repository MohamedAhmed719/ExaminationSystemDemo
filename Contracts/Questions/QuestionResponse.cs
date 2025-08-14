namespace ExaminationSystemDemo.Contracts.Questions;

public record QuestionResponse(
    int Id,
    string Content,
    string QuestionLevel,
    int ExamId,
    bool IsDeleted
    );