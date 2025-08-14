namespace ExaminationSystemDemo.Contracts.Students;

public record StudentAnswerRequest(
    int ChoiceId,
    int QuestionId
    );
