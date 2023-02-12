using IdealDiscuss.Entities;

namespace IdealDiscuss.Dto
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public String QuestionText { get; set; }
        public string ImageUrl { get; set; }
        public bool IsClosed { get; set; }
        public ICollection<CategoryQuestion> CategoryQuestions { get; set; } = new HashSet<CategoryQuestion>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
