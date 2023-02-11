namespace IdealDiscuss.Entities
{
    public class CategoryQuestion : BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
