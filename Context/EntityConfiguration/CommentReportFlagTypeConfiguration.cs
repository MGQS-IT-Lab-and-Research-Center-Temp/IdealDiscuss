using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IdealDiscuss.Context.EntityConfiguration
{
	public class CommentReportFlagTypeConfiguration : IEntityTypeConfiguration<CommentReportFlag>
	{
		public void Configure(EntityTypeBuilder<CommentReportFlag> builder)
		{
            builder.ToTable("CommentReportFlags");

            builder.HasKey(crf => new { crf.CommentReportId, crf.FlagId });

            builder.HasOne(crf => crf.CommentReport)
                .WithMany(cr => cr.CommentReportFlags)
                .HasForeignKey(crf => crf.CommentReportId)
                .IsRequired();

            builder.HasOne(crf => crf.Flag)
                .WithMany(f => f.CommentReportFlags)
                .HasForeignKey(crf => crf.FlagId)
                .IsRequired();
        }
	}

}
