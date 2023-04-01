using System.ComponentModel.DataAnnotations;

namespace IdealDiscuss.Dtos.CategoryDto
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
