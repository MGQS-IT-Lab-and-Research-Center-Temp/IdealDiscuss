using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IdealDiscuss.Context
{
    public class IdealDiscussContext : DbContext
    {
        public IdealDiscussContext(DbContextOptions<IdealDiscussContext> options) : base(options)
        {
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
    }
}
