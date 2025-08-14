namespace ExaminationSystemDemo.Errors;

public static class CourseErrors
{
    public static Error DuplicatedCourse = new("Course.DuplicatedCourseName", "Another course with the same name is alreadt exists");
    public static Error CourseNotFound = new("Course.CourseNotFound", "There was no course with the given id");
    public static Error DuplicatedEnrollmentApproval = new("Enrollment.DuplicatedEnrollmentApproval", "Student is Approved Already");
    public static Error EnrollmentNotFound = new("Enrollment.EnrollmentNotFound", "there was no student enroll in this course");
    public static Error InstructorNotAllowedToApprove = new("Instructor.InstructorNotAllowedToApprove", "You are not allowed to approve this enrollment");
    public static Error InstructorNotAllowedToReject = new("Instructor.InstructorNotAllowedToReject", "You are not allowed to reject this enrollment");
}
