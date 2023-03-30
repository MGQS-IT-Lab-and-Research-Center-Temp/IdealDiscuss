using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdealDiscuss.Context.EntityConfiguration
{
    public class CommentReportEntityTypeConfiguration : IEntityTypeConfiguration<CommentReport>
    {
        public void Configure(EntityTypeBuilder<CommentReport> builder)
        {
            builder.ToTable("CommentReports");

            builder.HasKey(cr => cr.Id);

            builder.Property(ac => ac.AdditionalComment)
                   .HasMaxLength(200);

            builder.HasOne(c => c.Comment)
                   .WithMany(cr => cr.CommentReports)
                   .HasForeignKey(c => c.CommentId)
                   .IsRequired();

            builder.HasOne(qr => qr.User)
                    .WithMany(u => u.CommentReports)
                    .HasForeignKey(qr => qr.UserId)
                    .IsRequired();

            builder.HasMany(c => c.CommentReportFlags)
                   .WithOne(cr => cr.CommentReport)
                   .IsRequired();
        }
    }
}

