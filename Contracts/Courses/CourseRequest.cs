namespace ExaminationSystemDemo.Contracts.Courses;

public record CourseRequest(
    string Name,
    int Hours,
    int Degree,
    int InstructorId
    );
