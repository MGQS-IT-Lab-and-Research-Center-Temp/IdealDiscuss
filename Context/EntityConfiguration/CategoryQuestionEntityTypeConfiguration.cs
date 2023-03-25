using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IdealDiscuss.Context.EntityConfiguration
{
	public class CategoryQuestionEntityTypeConfiguration : IEntityTypeConfiguration<CategoryQuestion>
	{
		public void Configure(EntityTypeBuilder<CategoryQuestion> builder)
		{
			builder.ToTable("CategoryQuestions");

			builder.HasKey(cq => new { cq.CategoryId, cq.QuestionId });
			builder.HasOne(cq => cq.Category)
				.WithMany(c => c.CategoryQuestions)
				.HasForeignKey(cq => cq.CategoryId);
			builder.HasOne(cq => cq.Question)
				.WithMany(q => q.CategoryQuestions)
				.HasForeignKey(cq => cq.QuestionId);
		}
	}

}
