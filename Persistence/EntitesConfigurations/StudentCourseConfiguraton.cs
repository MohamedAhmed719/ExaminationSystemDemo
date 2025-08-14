using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystemDemo.Persistence.EntitesConfigurations;

public class StudentCourseConfiguraton : IEntityTypeConfiguration<StudentCourse>
{
    public void Configure(EntityTypeBuilder<StudentCourse> builder)
    {
        builder.HasKey(x => new { x.StudentId, x.CourseId });
    }
}
