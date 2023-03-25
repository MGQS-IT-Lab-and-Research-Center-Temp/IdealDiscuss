using IdealDiscuss.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdealDiscuss.Context
{
	public class CategoryQuestionEntityTypeConfiguration : IEntityTypeConfiguration<CategoryQuestion>
	{
		public void Configure(EntityTypeBuilder<CategoryQuestion> builder)
		{
			builder.ToTable("CategoryQuestions");
			
		}
	}
}
