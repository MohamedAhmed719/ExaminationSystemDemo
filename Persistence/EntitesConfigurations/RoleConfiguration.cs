using ExaminationSystemDemo.Abstractions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystemDemo.Persistence.EntitesConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData([
            new IdentityRole<string>{
                Id = DefaultRoles.InstructorRoleId,
                ConcurrencyStamp= DefaultRoles.InstructorRoleConcurrenyStamp,
                Name = DefaultRoles.Instructor,
                NormalizedName = DefaultRoles.Instructor.ToUpper()
            },

            new IdentityRole<string>{
                Id = DefaultRoles.StudentrRoleId,
                ConcurrencyStamp= DefaultRoles.StudentRoleConcurrenyStamp,
                Name = DefaultRoles.Student,
                NormalizedName = DefaultRoles.Student.ToUpper()
            }
            ]);
    }
}
