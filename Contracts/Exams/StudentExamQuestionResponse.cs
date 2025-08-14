using ExaminationSystemDemo.Contracts.Choices;

namespace ExaminationSystemDemo.Contracts.Exams;

public record StudentExamQuestionResponse(
    int Id,
    string Content,
    string QuestionLevel,
    IEnumerable<ChoiceResponse> Choices
    );