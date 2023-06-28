using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

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

    public static void ConfigureDeletableEntities(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entity.ClrType))
            {
                modelBuilder
                   .Entity(entity.ClrType)
                   .HasQueryFilter(GetIsDeletedRestriction(entity.ClrType));
            }
        }
    }

    private static LambdaExpression GetIsDeletedRestriction(Type type)
    {
        var param = Expression.Parameter(type, "it");
        var prop = Expression.Call(_propertyMethod,
                                   param,
                                   Expression.Constant(IsDeletedProperty));
        var condition = Expression.MakeBinary(ExpressionType.Equal,
                                              prop,
                                              Expression.Constant(false));
        var lambda = Expression.Lambda(condition,
                                       param);
        return lambda;
    }

    private const string IsDeletedProperty = "IsDeleted";

    private static readonly MethodInfo _propertyMethod = typeof(EF).GetMethod(nameof(EF.Property),
                                                                              BindingFlags.Static |
                                                                              BindingFlags.Public)
                                                                   .MakeGenericMethod(typeof(bool));
}
