namespace IdealDiscuss.Dtos.CategoryDto
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
 
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
