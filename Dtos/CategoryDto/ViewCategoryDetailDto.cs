namespace IdealDiscuss.Dtos.CategoryDto
{
    public class ViewCategoryDetailDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
    }
}
