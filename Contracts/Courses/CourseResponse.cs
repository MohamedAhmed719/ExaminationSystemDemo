namespace ExaminationSystemDemo.Contracts.Courses;

public record CourseResponse(
    int Id,
    string Name,
    int Hours,
    int Degree,
    int InstructorId
    );
