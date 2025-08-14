namespace ExaminationSystemDemo.Errors;

public static class ExamErrors
{
    public static Error InstructorNotAllowed = new("Instructor.InstructorNotAllowed", "Instructor Not Allowed To Create This Exam");
    public static Error InstructorNotAllowedToEvaluateExam = new("Instructor.InstructorNotAllowedToEvaluateExam", "Instructor Not Allowed To Evaluate This Exam");
    public static Error ExamNotFound = new("Exam.ExamNotFound", "There was no exam with the given id");
    public static Error StudentNotEnorlledInCourse = new("Exam.StudentNotEnorlledInCourse", "you didn't enroll in the course");
}
