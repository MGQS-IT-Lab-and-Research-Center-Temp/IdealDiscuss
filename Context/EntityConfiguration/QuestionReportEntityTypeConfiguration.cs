using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace IdealDiscuss.Context
{
    public class QuestionReportEntityTypeConfiguration : IEntityTypeConfiguration<QuestionReport>
    {
        public void Configure(EntityTypeBuilder<QuestionReport> builder)
        {
            builder.ToTable("QuestionReport");

            builder.HasKey(qr => qr.Id);

            builder.Property(qr => qr.AdditionalComment)
                   .HasMaxLength(200);
                             
            builder.HasOne(qr => qr.Question)
                   .WithMany(q => q.QuestionReports)
                   .HasForeignKey(qr =>  qr.QuestionId)
                   .IsRequired();

            builder.HasOne(qr => qr.User)
                    .WithMany(u => u.QuestionReports)
                    .HasForeignKey(qr => qr.UserId)
                    .IsRequired();

            builder.HasMany(c => c.QuestionReportFlags)
                   .WithOne(cr => cr.QuestionReport)
                   .IsRequired();
        }
    }
}
