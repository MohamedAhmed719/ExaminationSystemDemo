namespace ExaminationSystemDemo.Contracts.Results;

public record StudentResultResponse(
    int Id,
    int Degree,
    bool IsPass,
    int StudentId,
    int Grade,
    string CourseName,
    string StudentName
    );
