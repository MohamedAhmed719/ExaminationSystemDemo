namespace ExaminationSystemDemo.Contracts.Exams;

public record ExamRequest(
     string Name,
     int CourseId,
     string ExamType,
     int NumberOfQuestions,
     DateOnly StartsAt,
     DateOnly EndsAt
    );