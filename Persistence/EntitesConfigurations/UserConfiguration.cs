using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExaminationSystemDemo.Persistence.EntitesConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(120);
        builder.Property(x => x.LastName).HasMaxLength(120);
    }
}
