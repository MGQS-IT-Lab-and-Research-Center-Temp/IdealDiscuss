namespace IdealDiscuss.Entities
{
    public class Question : BaseEntity
    {
        public int UserId { get; set; }
        public string QuestionText { get; set; }
        public string ImageUrl { get; set; }
        public bool IsClosed { get; set; }
        public ICollection<CategoryQuestion> CategoryQuestions { get; set; } = new HashSet<CategoryQuestion>();
    }
}
