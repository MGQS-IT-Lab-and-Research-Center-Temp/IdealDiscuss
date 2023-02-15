using IdealDiscuss.Entities;

namespace IdealDiscuss.Dtos.QuestionDto
{
    public class ViewQuestionDetailDto
    {
        public string QuestionText { get; set; }
        public string ImageUrl { get; set; }
        public bool IsClosed { get; set; }
        public DateTime LastModified { get; set; }
        public string CreatedBy { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public User User { get; set; }

    }
}
