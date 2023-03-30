namespace IdealDiscuss.Entities
{
    public class CategoryQuestion : BaseEntity
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string QuestionId { get; set; }
        public Question Question { get; set; }
    }
}