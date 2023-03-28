using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdealDiscuss.Context.EntityConfiguration
{
    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions");
            builder.HasKey(q => q.Id);

            builder.HasOne(q => q.User)
                .WithMany(u => u.Questions)
                .HasForeignKey(q => q.UserId)
                .IsRequired();

            builder.Property(q => q.QuestionText)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(q => q.ImageUrl)
                .HasColumnType("varchar(255)");

            builder.HasMany(q => q.CategoryQuestions)
                .WithOne(cq => cq.Question)
                .IsRequired();

            builder.HasMany(q => q.Comments)
                .WithOne(c => c.Question)
                .IsRequired();

            builder.HasMany(q => q.QuestionReports)
                .WithOne(qr => qr.Question)
                .IsRequired();
        }
    }
}
