namespace ExaminationSystemDemo.Errors;

public static class ChoiceErrors
{
    public static Error DuplicatedChoice = new("Choice.DuplicatedChoiceContent", "The same question with the same choice is already exists");
    public static Error ChoiceNotFound = new("Choice.ChoiceNotFound", "There was no choice with the given id");
}
