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

            builder.HasKey(u => u.UserId);

            builder.Property(q => q.QuestionText)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasMany(c => c.Comments)
                   .WithOne(q => q.Question)
                   .IsRequired();
    }
}
