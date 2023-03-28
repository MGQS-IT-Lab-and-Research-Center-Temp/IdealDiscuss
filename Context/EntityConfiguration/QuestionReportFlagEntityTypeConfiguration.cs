using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdealDiscuss.Context.EntityConfiguration
{
    public class QuestionReportFlagEntityTypeConfiguration : IEntityTypeConfiguration<QuestionReportFlag>
    {
        public void Configure(EntityTypeBuilder<QuestionReportFlag> builder)
        {
            builder.ToTable("QuestionReportFlags");
            builder.HasKey(qr => new { qr.QuestionReportId, qr.FlagId });
            builder.HasOne(qr => qr.QuestionReport)
                .WithMany(qrf => qrf.QuestionReportFlags)
                .HasForeignKey(qr => qr.QuestionReportId)
                .IsRequired();
            builder.HasOne(qf => qf.Flag)
                .WithMany(qrf => qrf.QuestionReportFlags)
                .HasForeignKey(qf => qf.FlagId)
                .IsRequired();
        }
    }
}
