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
            builder.Property(c => c.CommentReportFlags)
                .IsRequired();
            builder.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);
            builder.HasOne(c => c.Comment)
                .WithMany()
                .HasForeignKey(c => c.CommentId);
        }
    }
}

