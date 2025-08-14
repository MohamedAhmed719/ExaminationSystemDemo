namespace ExaminationSystemDemo.Contracts.Instructors;

public record InstructorRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Address,
    int Salary
    );
