namespace IdealDiscuss.Entities
{
    public class Question : BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string QuestionText { get; set; }
        public string ImageUrl { get; set; }
        public bool IsClosed { get; set; } 
        public ICollection<CategoryQuestion> CategoryQuestions { get; set; } = new HashSet<CategoryQuestion>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
