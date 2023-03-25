using Microsoft.Build.Framework;

namespace IdealDiscuss.Dtos.CategoryDto
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
