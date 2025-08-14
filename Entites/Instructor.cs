namespace ExaminationSystemDemo.Entites;

public class Instructor
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Salary { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;
    public List<Course> Courses { get; set; } = [];

}
