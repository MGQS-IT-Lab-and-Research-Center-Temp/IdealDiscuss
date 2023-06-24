using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdealDiscuss.Context;

public static class DbContextExtension
{
    public static void AddAuditInfo(this DbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        var entries = dbContext.ChangeTracker.Entries().Where(e =>
            e.Entity is IAuditBase
            && (e.State is EntityState.Added
            || e.State is EntityState.Modified
            || e.State is EntityState.Deleted));

        foreach (var entry in entries)
        {
            if (entry.State is EntityState.Added)
            {
                ((IAuditBase)entry.Entity).DateCreated = DateTime.UtcNow;
                ((IAuditBase)entry.Entity).CreatedBy = httpContextAccessor.HttpContext.User.Identity.Name ?? "System";
            }

            if (entry.State is EntityState.Modified || entry.State is EntityState.Deleted)
            {
                ((IAuditBase)entry.Entity).LastModified = DateTime.UtcNow;
                ((IAuditBase)entry.Entity).ModifiedBy = httpContextAccessor.HttpContext.User.Identity.Name ?? "System";
            }
        }
    }
}
