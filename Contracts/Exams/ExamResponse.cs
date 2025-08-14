namespace ExaminationSystemDemo.Contracts.Exams;

public record ExamResponse(
     string Name,
     int id,
     int CourseId,
     string ExamType,
     int NumberOfQuestions,
     DateOnly StartsAt,
     DateOnly EndsAt
    );
