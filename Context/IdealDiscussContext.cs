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
            /*builder.Entity<Category>().Property(c => c.Name)
                .IsRequired()
                .HasColumnName("CategoryName")
                .HasMaxLength(12);
            builder.Entity<Question>().Property(q => q.QuestionText)
                .IsRequired();
            builder.Entity<Category>().ToTable("MyCategory");
            builder.Entity<Question>().HasMany(q => q.Comments);
            builder.Entity<QuestionReportFlag>().HasOne(q => q.QuestionReport).WithMany().HasForeignKey(q => q.QuestionReportId);
            builder.Entity<QuestionReportFlag>().HasOne(q => q.Flag).WithMany().HasForeignKey(q => q.FlagId);*/
            //builder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
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
