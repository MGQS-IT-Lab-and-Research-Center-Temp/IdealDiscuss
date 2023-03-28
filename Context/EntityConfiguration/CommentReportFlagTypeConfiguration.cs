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

			builder.HasKey(cq => new { cq.CommentReportId, cq.FlagId });

			builder.HasOne(cq => cq.CommentReport)
				.WithMany(c => c.CommentReportFlags)
				.HasForeignKey(cq => cq.CommentReportId)
				.IsRequired();

			builder.HasOne(cq => cq.Flag)
				.WithMany(q => q.CommentReportFlags)
				.HasForeignKey(cq => cq.FlagId)
				.IsRequired();
		}
	}

}
