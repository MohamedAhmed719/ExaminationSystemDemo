namespace ExaminationSystemDemo.Contracts.Exams;

public record StudentExamResponse(
    string Name,
    ExamType ExamType,
    int NumberOfQuestions,
    DateOnly StartsAt,
    DateOnly EndsAt,
    IEnumerable<StudentExamQuestionResponse> Questons
    );
