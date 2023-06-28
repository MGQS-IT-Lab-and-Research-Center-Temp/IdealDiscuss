using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdealDiscuss.Context;

public class IdealDiscussContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdealDiscussContext(DbContextOptions<IdealDiscussContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Flag> Flags { get; set; }
    public DbSet<QuestionReport> QuestionReports { get; set; }
    public DbSet<QuestionReportFlag> QuestionReportFlags { get; set; }
    public DbSet<CategoryQuestion> CategoryQuestions { get; set; }
    public DbSet<CommentReport> CommentReports { get; set; }
    public DbSet<CommentReportFlag> CommentReportFlags { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateSoftDeleteStatuses();
        this.AddAuditInfo(_httpContextAccessor);
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateSoftDeleteStatuses();
        this.AddAuditInfo(_httpContextAccessor);
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private const string IsDeletedProperty = "IsDeleted";

    private void UpdateSoftDeleteStatuses()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.CurrentValues[IsDeletedProperty] = false;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.CurrentValues[IsDeletedProperty] = true;
                    break;
            }
        }
    }
}
