namespace ExaminationSystemDemo.Errors;

public static class QuestionErrors
{
    public static Error QuestionNotFound = new("Question.QuestionNotFound", "There was no question with the given id");
}
