namespace ExaminationSystemDemo.Persistence;
using  ExaminationSystemDemo.Entites;
using ExaminationSystemDemo.Extensions;
using System.Threading;
using System.Threading.Tasks;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IHttpContextAccessor httpContextAccessor,
    ILogger<ApplicationDbContext> logger) : IdentityDbContext<ApplicationUser>(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ILogger<ApplicationDbContext> _logger = logger;

    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Choice> Choices { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<StudentResult> StudentResults { get; set; }
    public DbSet<StudentAnswer> StudentAnswer { get; set; }
    public DbSet<StudentCourse> StudentCourse { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        var FKsCascade = modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys())
            .Where(f => f.DeleteBehavior == DeleteBehavior.Cascade && !f.IsOwnership);

        foreach (var fk in FKsCascade)
            fk.DeleteBehavior = DeleteBehavior.Restrict;

        base.OnModelCreating(modelBuilder);

    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        var entries = ChangeTracker.Entries<AudtitableEntity>();

        foreach(var entry in entries)
        {

            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();

            if(entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedById).CurrentValue = currentUserId!;
            } 
            else if(entry.State == EntityState.Modified)
            {
                entry.Property(x => x.UpdatedById).CurrentValue = currentUserId;
                entry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;

            }

        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
