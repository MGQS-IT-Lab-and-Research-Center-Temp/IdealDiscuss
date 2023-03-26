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
            builder.HasKey(cr => new { cr.CommentId, cr.UserId });
            builder.Property(cr => cr.CommentReportFlags)
                   .IsRequired();
            builder.HasOne(c => c.Comment);
            builder.HasOne(u => u.User);
        }
    }
}

